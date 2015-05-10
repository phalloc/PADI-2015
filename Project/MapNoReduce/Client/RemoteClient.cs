using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class RemoteClient : MarshalByRefObject, IClient
    {
        
        private string jobFilePath;
        private string destPath;
        private long nSplits;
        UTF8Encoding encoding;
        private delegate void AsyncWriteToFileCaller(IList<KeyValuePair<string, string>> Map, long splitId);
       
        
        public RemoteClient(string jobFilePath, long nSplits, string destPath)
        {
            this.jobFilePath = jobFilePath;
            this.nSplits = nSplits;
            this.destPath = destPath;
            encoding = new UTF8Encoding(true);
        }


        public byte[] getWorkSplit(long beginSplit, long endSplit)
        {
            Logger.LogInfo("Received request from node: (start, end) = (" + beginSplit + "," + endSplit + ")");
            byte[] splitGiven;


                FileReader fileReader = new FileReader(jobFilePath);
                try
                {
                    splitGiven = fileReader.fetchSplitFromFile(beginSplit, endSplit);
                    //Logger.LogInfo("Split: " + splitGiven);
                    //Logger.LogInfo(splitGiven.ToCharArray().Length + "");
                }
                catch (Exception ex)
                {
                    Logger.LogErr("Client error reading from " + beginSplit + " to " + endSplit);
                    throw ex;
                }
            fileReader.closeReader();

            Logger.LogInfo("Returning from node: (start, end) = (" + beginSplit + "," + endSplit + ")");
            
            return splitGiven;
        }

        public void returnWorkSplit(IList<KeyValuePair<string, string>> Map, long splitId)
        {

            Logger.LogInfo("Received split number " + splitId);

            //Async
            AsyncWriteToFileCaller caller = new AsyncWriteToFileCaller(this.WriteMapToFile);
            caller.BeginInvoke(Map, splitId, null, null);
            //Sync
            //WriteMapToFile(Map, splitId);

            nSplits--;

            if (nSplits <= 0)
            {
                Logger.LogInfo("Work done!");
            }

        }

        private void WriteMapToFile(IList<KeyValuePair<string, string>> Map, long splitId)
        {
            StreamWriter writer = File.CreateText(destPath + "/" + splitId + ".out");
            foreach (KeyValuePair<string, string> entry in Map)
            {
                string key = entry.Key;
                string value = entry.Value;

                string line = "key: " + key + " value: " + value + "\n";
                //Logger.LogInfo("line: " + line);
                writer.Write(line);

            }

            writer.Close();
        }

        public override object InitializeLifetimeService()
        {

            return null;

        }


    }
}
