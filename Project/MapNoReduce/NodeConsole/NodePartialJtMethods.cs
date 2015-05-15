using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace PADIMapNoReduce
{
    //Job tracker portion of the node
    partial class Node
    {
        private static int PingTimeout = 4000;
        private JobTrackerInformation jtInformation;
        IWorker primaryJobTracker;
        IWorker secondaryJT = null;
                
        private bool receivedAliveFromServer;

        bool isPrimary = false;
        bool didStartedPrimaryProcess = false;

        public bool PingJT()
        {
            /* wait until if I am unfrozen and revert to Worker if needed */
            if (WaitForUnfreezeAndCheckChanges())
                return false;

            Logger.LogInfo("Answering Ping");
            return true;
        }

        public void SetUpAsSecondaryServer(string primaryJTurl, long numSplits) {

            isPrimary = false;
            primaryJobTracker = (IWorker)Activator.GetObject(typeof(IWorker), primaryJTurl);
            jtInformation = new JobTrackerInformation(this, numSplits);
            Thread SendIAmAliveThread = new Thread(() =>
            {
                while (!isPrimary)
                {
                    /* wait until if I am unfrozen and revert to Worker if needed */
                    if (WaitForUnfreezeAndCheckChanges())
                        break;
                    /* --------------------------- */

                    Logger.LogInfo("Sending I am alive");
                    try
                    {
                        primaryJobTracker.PingJT();
                        receivedAliveFromServer = true;
                    }
                    catch (Exception)
                    {
                        Logger.LogErr("TIMEOUT");
                    }


                    if (!receivedAliveFromServer)
                    {
                        Logger.LogErr("PRIMARY JOB TRACKER IS DOWN");
                        nodeDown();
                        PrintUpdateNetwork();
                        jtInformation.AlertChangeOfJobTracker(myURL);
                        this.StartPrimaryJobTrackerProcess(jtInformation.numSplits);
                        break;
                    }
                    else
                    {
                        Logger.LogErr("PRIMARY JOB TRACKER IS UP!!!");
                    }

                    receivedAliveFromServer = false;
                    Thread.Sleep(PingTimeout);
                    
                }
            });
            SendIAmAliveThread.Start();
        }

        private void StartPrimaryJobTrackerProcess(long numSplits){
            currentJobTrackerUrl = this.myURL;
            serverRole = ServerRole.JOB_TRACKER;
            status = ExecutionState.WORKING;
            isPrimary = true;
            jtInformation = new JobTrackerInformation(this, numSplits);
            
            Thread trackWorkersThread = new Thread(() =>
            {
                while (!jtInformation.DidFinishJob() && isPrimary && serverRole == ServerRole.JOB_TRACKER)
                {
                    /* wait until if I am unfrozen and revert to Worker if needed */
                    if (WaitForUnfreezeAndCheckChanges())
                        break;
                    /* --------------------------- */

                    Logger.LogInfo("[CHECKING SLOW WORKERS]");
                    SplitInfo slowSplit = jtInformation.FindSlowSplit();
                    if (slowSplit != null)
                    {
                        Logger.LogWarn("There is a slow split - " + slowSplit.splitId);
                        IWorker freeWorker = jtInformation.GetFirstFreeWorker();
                        if (freeWorker != null)
                        {
                            Logger.LogWarn("[SLOWWWWWWW SPLIT] RESENDING " + slowSplit.remainingSplits);
                            ResendSplitToNextWorker(freeWorker, slowSplit.totalSplits, slowSplit.remainingSplits);
                        }
                    }
                    Thread.Sleep(2000);
                }
                //TIAGO SANTOS PODES POR AQUI O QUE QUISERES PARA NOTIFICAR O CLIENTE QUE ACABOU
            });
            trackWorkersThread.Start();

            Thread ConfigureSecondaryServerThread = new Thread(() =>
            {
                //wait for an available backUrl, then pings it then sets up as primary Server
                while (isPrimary && serverRole == ServerRole.JOB_TRACKER)
                {
                    if (backURL == myURL)
                    {
                        continue;
                    }

                    Logger.LogInfo("Waiting for an available url to be the secondary JT");
                    secondaryJT = (IWorker)Activator.GetObject(typeof(IWorker), backURL);

                    try
                    {
                        secondaryJT.PingJT();
                        secondaryJT.SetUpAsSecondaryServer(this.myURL, numSplits);
                        Logger.LogInfo("Success setting setup backupserver");
                        break;
                    }
                    catch(Exception)
                    {
                        Logger.LogInfo("There is still no backUrl available to become backup server");
                    }

                    Thread.Sleep(1000);
                }

            });
            ConfigureSecondaryServerThread.Start();
        }


        public void ReceiveWork(string clientURL, long fileSize, long splits, string mapperName, byte[] mapperCode)
        {
            try
            {
                Logger.LogInfo("Received: " + clientURL + " with " + splits + " splits fileSize =" + fileSize);


                this.clientURL = clientURL;
                client = (IClient)Activator.GetObject(typeof(IClient), clientURL);

                if (!didStartedPrimaryProcess)
                {
                    StartPrimaryJobTrackerProcess(splits);
                    didStartedPrimaryProcess = true;
                }


                /* wait until if I am unfrozen and revert to Worker if needed */
                WaitForUnfreezeAndCheckChanges();
                /* --------------------------- */



                IWorker nextWorker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                if (splits > fileSize)
                    splits = fileSize;
                long splitSize = fileSize / splits;



                FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(nextWorker.FetchWorker);
                IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, myURL, mapperName, mapperCode, fileSize, splits, splits, myURL, true, null, null);

                return;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }
        }

        public void RegisterWorker(string workerId, string workerUrl)
        {
            /* wait until if I am unfrozen and revert to Worker if needed */
            WaitForUnfreezeAndCheckChanges();
            /* --------------------------- */

            if (isPrimary)
            {
                Logger.LogInfo("Resending register to secondary JT");
                secondaryJT.RegisterWorker(workerId, workerUrl);
            }
            Logger.LogInfo("REGISTERING WORKER");
            IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), workerUrl);
            jtInformation.RegisterWorker(workerId, worker);
        }

        public void UnregisterWorker(string workerId)
        {
            /* wait until if I am unfrozen and revert to Worker if needed */
            WaitForUnfreezeAndCheckChanges();
            /* --------------------------- */

            jtInformation.UnregisterWorker(workerId);
        }

        public void LogStartedSplit(string workerId, long fileSize, long totalSplits, long remainingSplits)
        {
            /* wait until if I am unfrozen and revert to Worker if needed */
            WaitForUnfreezeAndCheckChanges();
            /* --------------------------- */

            if (isPrimary)
            {
                Logger.LogInfo("RESENDING STARTSPLIT TO SECONDARY SERVER");
                secondaryJT.LogStartedSplit(workerId, fileSize, totalSplits, remainingSplits);
            }
            jtInformation.LogStartedSplit(workerId, fileSize, totalSplits, remainingSplits);
        }
        
        public void LogFinishedSplit(string workerId, long totalSplits, long remainingSplits)
        {
            /* wait until if I am unfrozen and revert to Worker if needed */
            WaitForUnfreezeAndCheckChanges();
            /* --------------------------- */

            if (isPrimary)
            {
                Logger.LogInfo("RESENDING ENDSPLIT TO SECONDARY SERVER");
                secondaryJT.LogFinishedSplit(workerId, totalSplits, remainingSplits);
            }
            jtInformation.LogFinishedSplit(workerId, totalSplits, remainingSplits);
        }
        public bool IsPrimary()
        {
            return isPrimary;
        }

        public bool  WaitForUnfreezeAndCheckChanges()
        {
            WaitForUnfreeze();
            if (secondaryJT != null && secondaryJT.IsPrimary())
            {
                Logger.LogWarn("REVERTING BACK TO WORKER");
                this.serverRole = ServerRole.WORKER;
                isPrimary = false;
                didStartedPrimaryProcess = false;
                return true;
            }

            return false;

        }

        private void ResendSplitToNextWorker(IWorker worker, long totalSplits, long remainingSplits)
        {

            FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);
            Thread liveCheck = new Thread(this.liveCheck);
            IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, myURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, myURL, false, null, null);
            liveCheck.Start(RemAr);
        }
    }
}
