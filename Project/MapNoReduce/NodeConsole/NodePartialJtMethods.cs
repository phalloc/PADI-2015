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
        
        public bool PingJT()
        {
            Logger.LogInfo("Answering Ping");
            return true;
        }

        public void SetUpAsSecondaryServer(string primaryJTurl, long numSplits) {
            primaryJobTracker = (IWorker)Activator.GetObject(typeof(IWorker), primaryJTurl);
            jtInformation = new JobTrackerInformation(this, numSplits);
            Thread SendIAmAliveThread = new Thread(() =>
            {
                while (true)
                {
                    /* wait until if I am unfrozen */
                    WaitForUnfreeze();
                    /* --------------------------- */

                    Logger.LogInfo("Sending I am alive");
                    try
                    {
                        primaryJobTracker.PingJT();
                        receivedAliveFromServer = true;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogErr("TIMEOUT");
                    }


                    if (!receivedAliveFromServer)
                    {
                        Logger.LogErr("PRIMARY JOB TRACKER IS DOWN");
                        nodeDown();
                        PrintUpdateNetwork();
                        jtInformation.AlertChangeOfJobTracker(myURL);
                        this.StartJobTrackerProcess(jtInformation.numSplits);
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

        private void StartJobTrackerProcess(long numSplits){
            jtInformation = new JobTrackerInformation(this, numSplits);

            Thread trackWorkersThread = new Thread(() =>
            {
                while (!jtInformation.DidFinishJob())
                {
                    /* wait until if I am unfrozen */
                    WaitForUnfreeze();
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
                            ResentSplitToNextWorker(freeWorker, slowSplit.totalSplits, slowSplit.remainingSplits);
                        }
                    }
                    Thread.Sleep(2000);
                }
                //TIAGO SANTOS PODES POR AQUI O QUE QUISERES PARA NOTIFICAR O CLIENTE QUE ACABOU
            });
            trackWorkersThread.Start();

            Thread ConfigureSecondaryServerThread = new Thread(() =>
            {
                while (true)
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
                /* wait until if I am unfrozen */
                WaitForUnfreeze();
                /* --------------------------- */


                Logger.LogInfo("Received: " + clientURL + " with " + splits + " splits fileSize =" + fileSize);

                currentJobTrackerUrl = this.id;
                serverRole = ServerRole.JOB_TRACKER;
                status = ExecutionState.WORKING;
                this.clientURL = clientURL;
                client = (IClient)Activator.GetObject(typeof(IClient), clientURL);


                StartJobTrackerProcess(splits);


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
            Logger.LogInfo("REGISTERING WORKER");
            IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), workerUrl);
            jtInformation.RegisterWorker(workerId, worker);
        }

        public void UnregisterWorker(string workerId)
        {
            jtInformation.UnregisterWorker(workerId);
        }

        public void LogStartedSplit(string workerId, long fileSize, long totalSplits, long remainingSplits)
        {
            Logger.LogInfo("RESENDING STARTSPLIT TO SECONDARY SERVER");
            //secondaryJT.LogStartedSplit(workerId, fileSize, totalSplits, remainingSplits);
            jtInformation.LogStartedSplit(workerId, fileSize, totalSplits, remainingSplits);
        }
        
        public void LogFinishedSplit(string workerId, long totalSplits, long remainingSplits)
        {
            Logger.LogInfo("RESENDING ENDSPLIT TO SECONDARY SERVER");
            //secondaryJT.LogFinishedSplit(workerId, totalSplits, remainingSplits);
            jtInformation.LogFinishedSplit(workerId, totalSplits, remainingSplits);
        }

        public void ResentSplitToNextWorker(IWorker worker, long totalSplits, long remainingSplits)
        {
            FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);
            Thread liveCheck = new Thread(this.liveCheck);
            IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, myURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, myURL, false, null, null);
            liveCheck.Start(RemAr);
        }
    }
}
