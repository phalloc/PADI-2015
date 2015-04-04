using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class PuppetMaster
    {
        private CommandsManager cm = new CommandsManager();
        private IDictionary<string, string> knownWorkers = new Dictionary<string, string>();
        private string serviceUrl;

        public void LoadConfigurationFile(IDictionary<string, string> dic)
        {
            this.serviceUrl = dic["SERVICE_URL"];
            dic.Remove("SERVICE_URL");
    
            this.knownWorkers = dic;

            Logger.LogInfo("LOADED CONFIGURATION FILE");
        }

        public void LoadFile(string file)
        {
            cm.LoadFile(file);
        }

        public void ExecuteScript()
        {
            cm.ExecuteScript();
        }

        public void ExecuteCommand(string line)
        {
            cm.ExecuteCommand(line);
        }
        
    }
}
