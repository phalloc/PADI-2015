using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class RemoteClient : MarshalByRefObject, IClient
    {
        FormRemoteGUI form = null;

        private string filePath;

        public RemoteClient(string filePath)
        {
            this.filePath = filePath;
        }


        public void getWorkSplit()
        {
            System.Diagnostics.Debug.WriteLine("received request from node");
            //dar um split ao worker
        }

        public void returnWorkSplit()
        {
            // retornar o trabalho feito
        }

    }
}
