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
        
        private FileReader fileReader;
        private string destPath;
        private int nSplits;
        UTF8Encoding encoding;
       
        
        public RemoteClient(FileReader reader, int nSplits, string destPath)
        {
            this.fileReader = reader;
            this.nSplits = nSplits;
            this.destPath = destPath;
            encoding = new UTF8Encoding(true);
        }


        public string getWorkSplit(long beginSplit, long endSplit)
        {

            

            Logger.LogInfo("received request from node");
            Logger.LogErr("received request from node");
            Logger.LogWarn("received request from node");

            return fileReader.fetchSplitFromFile(beginSplit, endSplit);
        }

        public void returnWorkSplit(IList<KeyValuePair<string, string>> Map, int splitId)
        {
            //TODO: perceber como raio vamos parar isto quando ja nao houver mais splits
            // ah e testar
            Logger.LogInfo("Received split number " + splitId);

            FileStream fs = File.Create(destPath + "/" + splitId + ".out");

            foreach(KeyValuePair<string, string> entry in Map){
                string key = entry.Key;
                string value = entry.Value;

                string line = "key: " + key + " value: " + value + "\n";

                byte[] lineBytes = encoding.GetBytes(line.ToCharArray(), 0, line.Length);

                fs.Write(lineBytes, 0, lineBytes.Length);
            }
            
            nSplits--;

            if (nSplits <= 0)
            {
                Logger.LogInfo("Work done!");

            }

        }

    }
}
