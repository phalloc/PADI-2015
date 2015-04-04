using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class PM : MarshalByRefObject, IPuppetMaster
    {

        private string workerExecutableDirectoy;


        public void SetworkerExecutableDirectoy(string workerExecutableDirectoy)
        {
            this.workerExecutableDirectoy = workerExecutableDirectoy;
        }


        //FIXME FALTA LANÇAR EXCEPCOES REMOTAS CASO O workerExecutableDirectory nao estiver definido
        public void CreateWorker(string id, string serviceUrl, string entryUrl)
        {
            Logger.LogInfo("RECEIVED REQUEST TO CREATE WORKER");
            Logger.LogInfo("id: " + id);
            Logger.LogInfo("serviceUrl: " + serviceUrl);
            Logger.LogInfo("entryUrl: " + entryUrl);



            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = workerExecutableDirectoy;
            p.StartInfo.Arguments = "/c echo Foo && echo Bar";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardOutput.ReadToEnd().Dump();
        }
    }
}
