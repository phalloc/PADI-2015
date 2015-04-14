using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;
using System.Threading;

namespace PADIMapNoReduce
{
    public class PuppetMaster
    {
        private CommandsManager cm;


        public static bool Run_Script_Step_By_Step_Opt = false;
        private string serviceUrl;
        private static string SERVICE_NAME = "PM";
        private int port;

        private PM remoteObject;


        public PuppetMaster(){
            cm = new CommandsManager(this);
        }

        public void RegisterWorkers(IDictionary<string, string> dic)
        {
            foreach (KeyValuePair<string, string> entry in dic)
            {
                string id = entry.Key;
                string url = entry.Value;
                NetworkManager.RegisterNewWorker(id, url);
            }


            Logger.RefreshNetwork();
        }

        public void StartService(string url)
        {
            string[] splits = url.Split(':');
            splits = splits[splits.Length - 1].Split('/');
            int channelPort = int.Parse(splits[0]);

            try { 
                this.port = channelPort;

                remoteObject = new PM();
                TcpChannel myChannel = new TcpChannel(this.port);
                ChannelServices.RegisterChannel(myChannel, true);
                RemotingServices.Marshal(remoteObject, SERVICE_NAME, typeof(IPuppetMaster));

                serviceUrl = "tcp://localhost:" + this.port + "/" + SERVICE_NAME;
                Logger.LogInfo("Started PuppetMaster service @ " + serviceUrl);
            }
            catch (Exception ex)
            {
                Logger.LogErr("Couldn't start service: " + ex.Message);
            }
        }

        /**************** COMMANDS **********************/
        
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

        public void ProcessNextCommand()
        {
            cm.ProcessNextCommand();
        }
    }
}
