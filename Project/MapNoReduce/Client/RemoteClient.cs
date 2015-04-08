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
        
        private string filePath;
        private byte[] mapperCode;
        private string mapperName;

        public RemoteClient(string filePath, string mapperName, string mapperPath)
        {
            this.filePath = filePath;
            this.mapperCode = File.ReadAllBytes(mapperPath);
            this.mapperName = mapperName;


        }


        public void getWorkSplit()
        {
            Logger.LogInfo("received request from node");
            Logger.LogErr("received request from node");
            Logger.LogWarn("received request from node");

            //dar um split ao worker
        }

        public void returnWorkSplit()
        {
            // retornar o trabalho feito
        }

    }
}
