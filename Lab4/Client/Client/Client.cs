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

namespace ClientInterface
{
    public class Client
    {
        AgentStorage agentObj = null;

        private bool setupRemote()
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, true);

            agentObj = (AgentStorage)Activator.GetObject(
                typeof(AgentStorage),
                "tcp://localhost:8086/AgentStorage");

            if (agentObj == null)
                return false;
            else
                return true;

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

            // insert remote exception

            return agentObj.getPerson(agentID);
        }

    }
}
