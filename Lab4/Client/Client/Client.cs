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

        public bool registerPerson(string name, int age, int agentID)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, true);

            AgentStorage agentObj = (AgentStorage)Activator.GetObject(
                typeof(AgentStorage),
                "tcp://localhost:8086/AgentStorage");

            if (agentObj == null)
                return false;

            agentObj.registerPerson(new Person(name, age, agentID));

            return true;

        }

        public Person retrievePerson(int agentID)
        {

            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, true);

            AgentStorage agentObj = (AgentStorage)Activator.GetObject(
                typeof(AgentStorage),
                "tcp://localhost:8086/AgentStorage");

            // insert remote exception

            return agentObj.getPerson(agentID);
        }

    }
}
