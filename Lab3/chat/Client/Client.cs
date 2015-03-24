using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;

using System.Windows.Forms;


namespace RemotingSample
{
    public class Client{

        private ObjServer serverObj = null;
        private string nick = null;

        private FormRemoteGUI formClient = null;
        public Client(FormRemoteGUI f)
        {
            formClient = f;
        }

        //HANDLER DO FORM
        public bool SendMessage(string msg)
        {
            serverObj.SendMsg(nick, msg);
            return true;
        }

        //HANDLER DO FORM
        public bool Register(string nick, string port)
        {
            this.nick = nick;

            TcpChannel channel = new TcpChannel(Int32.Parse(port));
            ChannelServices.RegisterChannel(channel, true);

            ObjClient objClient = new ObjClient(formClient);
            RemotingServices.Marshal(objClient,
                "IChatClient",
                typeof(ObjClient));

            string serverUrl = "tcp://localhost:8086/IChatServer";

            serverObj = (ObjServer)Activator.GetObject(
                typeof(ObjServer), serverUrl);

            if (serverObj == null) { 
                System.Console.WriteLine("Could not locate server");
                return false;
            }

            string clientUrl = "tcp://localhost:" + port + "/IChatClient";
            serverObj.Register(nick, clientUrl);

            return true;
        }

		static void Main() {
            System.Console.WriteLine("waiting for user");
            System.Console.ReadLine();
        }
	}

}