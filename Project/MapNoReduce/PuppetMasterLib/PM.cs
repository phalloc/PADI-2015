﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PADIMapNoReduce
{
    public class PM : MarshalByRefObject, IPuppetMaster
    {
        //FIXME FALTA LANÇAR EXCEPCOES REMOTAS CASO O workerExecutableDirectory nao estiver definido
        public void CreateWorker(string id, string serviceUrl, string entryUrl)
        {
            Logger.LogInfo("Creating \'" + id + "\' at " + serviceUrl + " with entry url " + entryUrl);

            string arguments = id + " " + serviceUrl + " " + entryUrl;
            ProcessUtil.ExecuteNewProcess(PuppetMaster.GetWorkerExeLocation(), arguments);
            PuppetMaster.RegisterNewWorker(id, serviceUrl);
        }
    }
}
