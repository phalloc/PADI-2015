using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using CommonLib;
using Server;
using System.Runtime.Remoting.Messaging;

namespace ClientInterface
{
    public class Client
    {
        AgentStorage agentObj = null;

        public delegate Person RevivePersonAsyncDelegate(int agentID);

        private bool setupRemote()
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, true);

            agentObj = (AgentStorage)Activator.GetObject(
                typeof(AgentStorage),
                "tcp://localhost:8086/AgentStorage");



            return agentObj != null;
        }

        public bool registerPerson(string name, int age, int agentID)
        {


            if (agentObj == null) 
            {
                if (setupRemote() == false)
                    return false;
            }

            agentObj.registerPerson(new Person(name, age, agentID));
           

            return true;

        }

        public Person retrievePerson(int agentID)
        {

            if (agentObj == null)
            {
                if (setupRemote() == false)
                    return null;//well sh*t
            }

            RevivePersonAsyncDelegate RemoteDel = new RevivePersonAsyncDelegate(agentObj.getPerson);
            // Call remote method without callback
            IAsyncResult RemAr = RemoteDel.BeginInvoke(agentID,null, null);
            // Wait for the end of the call and then explictly call EndInvoke
            RemAr.AsyncWaitHandle.WaitOne();
            return RemoteDel.EndInvoke(RemAr);
        }

    }
}
