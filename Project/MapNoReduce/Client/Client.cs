using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;


namespace PADIMapNoReduce
{
    public class Client
    {
        private IWorker worker = null;
        private static string SERVICE_NAME = "C";
        private static int DEFAULT_PORT = 10001;
        private string clientURL = "tcp://localhost:" + DEFAULT_PORT + "/" + SERVICE_NAME;


        public void submitJob(string jobFilePath, string destPath, string entryUrl, long splits, string mapperName, string mapperPath)
        {
            Logger.LogInfo("---------------------------------------");
            Logger.LogInfo("---------------------------------------");
            Logger.LogInfo("jobFilePath: " + jobFilePath);
            Logger.LogInfo("destPath: " + destPath);
            Logger.LogInfo("entryUrl: " + entryUrl);
            Logger.LogInfo("splits: " + splits);
            Logger.LogInfo("mapperName: " + mapperName);
            Logger.LogInfo("mapperPath: " + mapperPath);
            Logger.LogInfo("---------------------------------------");
            Logger.LogInfo("---------------------------------------");


            byte[] mapperCode = File.ReadAllBytes(mapperPath);
            FileReader reader = new FileReader(jobFilePath);
            long fileSize = reader.getFileSize();

            if (splits > fileSize)
                splits = fileSize;

            try
            {
                TcpChannel channel = new TcpChannel(DEFAULT_PORT);
                ChannelServices.RegisterChannel(channel, true);

                RemoteClient rmClient = new RemoteClient(reader, splits, destPath);
                RemotingServices.Marshal(rmClient, SERVICE_NAME, typeof(IClient));

                worker = (IWorker)Activator.GetObject(typeof(IWorker), entryUrl);
                if (worker == null)
                {
                    Logger.LogErr("Could not find specified worker");
                    return;
                }

                else worker.ReceiveWork(clientURL, fileSize, splits, mapperName, mapperCode);
            }
            //catched when node is not responding
            catch (RemotingTimeoutException timeException)
            {
                System.Diagnostics.Debug.WriteLine("time exception: " + timeException.Message);
            }
        }
    }
}
