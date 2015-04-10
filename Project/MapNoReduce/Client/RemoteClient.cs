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
        private static Object _lock = new Object();
       
        
        public RemoteClient(FileReader reader, int nSplits, string destPath)
        {
            this.fileReader = reader;
            this.nSplits = nSplits;
            this.destPath = destPath;
            encoding = new UTF8Encoding(true);
        }


        public string getWorkSplit(long beginSplit, long endSplit)
        {
            Logger.LogInfo("Received request from node: (start, end) = (" + beginSplit + "," + endSplit + ")");
            string splitGiven;
               
            lock (_lock)
                
            {
                try { 
                    splitGiven = fileReader.fetchSplitFromFile(beginSplit, endSplit);
                    Logger.LogInfo("Split given: " + splitGiven);
                }
                catch(Exception ex) {
                    Logger.LogErr(ex.Message);
                    throw ex;
                }             
            }
           
            return splitGiven;
        }

        public void returnWorkSplit(IList<KeyValuePair<string, string>> Map, int splitId)
        {
            //TODO: perceber como raio vamos parar isto quando ja nao houver mais splits
            // ah e testar
            Logger.LogInfo("Received split number " + splitId);

            foreach(KeyValuePair<string, string> entry in Map){
                string key = entry.Key;
                string value = entry.Value;

                string line = "key: " + key + " value: " + value + "\n";
                Logger.LogInfo("line: " + line);
                File.WriteAllLines(destPath + "/" + splitId + ".out", new string[] { line });

            }
            
            nSplits--;

            if (nSplits <= 0)
            {
                Logger.LogInfo("Work done!");
            }

        }

    }
}
