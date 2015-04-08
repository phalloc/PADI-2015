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
        private int nSplits;

        public RemoteClient(string filePath, string mapperName, string mapperPath, int nSplits)
        {
            this.filePath = filePath;
            this.mapperCode = File.ReadAllBytes(mapperPath);
            this.mapperName = mapperName;
            this.nSplits = nSplits;

        }


        public void getWorkSplit()
        {

            //TODO: 2 argumentos a serem usados

            Logger.LogInfo("received request from node");
            Logger.LogErr("received request from node");
            Logger.LogWarn("received request from node");

            //dar um split ao worker
        }

        public void returnWorkSplit()
        {
            //TODO: return deve devolver o mapa e o numero do splits
            //receber mapa do worker e escrever .out
            //incrementar numero de splits recebidos

            nSplits--;


            // retornar o trabalho feito
        }

    }
}
