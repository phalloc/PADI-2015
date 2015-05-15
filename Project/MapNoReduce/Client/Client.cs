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

            foreach (string file in Directory.GetFiles(destPath, "*.out"))
            {
                File.Delete(file);
            }

            byte[] mapperCode = File.ReadAllBytes(mapperPath);

            FileReader reader = new FileReader(jobFilePath);
            long fileSize = reader.getFileSize();
            reader.closeReader();

            if (splits > fileSize)
                splits = fileSize;

            try
            {
                TcpChannel channel = new TcpChannel(DEFAULT_PORT);
                ChannelServices.RegisterChannel(channel, true);

                RemoteClient rmClient = new RemoteClient(jobFilePath, splits, destPath);
                RemotingServices.Marshal(rmClient, SERVICE_NAME, typeof(IClient));

                worker = (IWorker)Activator.GetObject(typeof(IWorker), entryUrl);
                if (worker == null)
                {
                    Logger.LogErr("Could not find specified worker");
                    return;
                }

                Logger.LogInfo("Submitting job to: " + entryUrl);
                worker.ReceiveWork(clientURL, fileSize, splits, mapperName, mapperCode);
            }
            //catched when node is not responding
            catch (RemotingTimeoutException timeException)
            {
                Logger.LogErr("time exception: " + timeException.Message);
            }
        }
    }
}
