using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class RemoteClient : MarshalByRefObject, IClient
    {
        private static Logger logger;
        
        private string filePath;

        public RemoteClient(string filePath, FormRemoteGUI form)
        {
            this.filePath = filePath;
            logger = new Logger(form);
        }


        public void getWorkSplit()
        {
            logger.LogInfo("received request from node");
            logger.LogErr("received request from node");
            logger.LogWarn("received request from node");

            //dar um split ao worker
        }

        public void returnWorkSplit()
        {
            // retornar o trabalho feito
        }

    }
}
