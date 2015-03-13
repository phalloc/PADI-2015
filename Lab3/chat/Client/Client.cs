using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;

namespace RemotingSample {

    public delegate void MsgDelegate(string msg);

	public class Client{

        private ObjServer obj = null;
        private string nick = null;

        MsgDelegate msgDel;

        
        public void handler_writeInForm(string msg)
        {
            System.Console.WriteLine("Formating message " + msg);
            //updates the form textbox               
        }

        public bool SendMessage(string msg)
        {
            System.Console.WriteLine("Sending > Server : " + msg);
            obj.SendMsg(nick, msg);
            return true;
        }

        public bool Register(string nick, string port)
        {
            msgDel = new MsgDelegate(handler_writeInForm);
            this.nick = nick;

            TcpChannel channel = new TcpChannel(Int32.Parse(port));
            ChannelServices.RegisterChannel(channel, true); //NO EXEMPLO DO PROF ESTAVA A FALSE http://groups.ist.utl.pt/meic-padi/labs/aula3/aula3-slides.pdf

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(ObjClient),
                "IChatClient",
                WellKnownObjectMode.Singleton);

            string serverUrl = "tcp://localhost:8086/IChatServer";

            obj = (ObjServer)Activator.GetObject(
                typeof(ObjServer), serverUrl);

            if (obj == null) { 
                System.Console.WriteLine("Could not locate server");
                return false;
            }

            System.Console.WriteLine("Registering in the server...");
            string clientUrl = "tcp://localhost:" + port + "/IChatClient";
            obj.Register(nick, clientUrl);

            return true;
        }

		static void Main() {
            System.Console.WriteLine("waiting for user");
            System.Console.ReadLine();
        }
	}

}