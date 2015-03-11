using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;

namespace RemotingSample {

	public class Client : MarshalByRefObject, IChatClient{

        private TcpChannel channel = null;
        private IChatServer obj = null;
        private string nick = null;


        public bool RecvMsg(string msg)
        {
            System.Console.WriteLine(msg);
            return true;
        }

        public bool SendMessage(string msg)
        {
            obj.SendMsg(nick, msg);
            return true;
        }

        public bool Register(string nick, string port)
        {
            System.Console.WriteLine("registering");
            this.nick = nick;
            channel = new TcpChannel(Convert.ToInt32(port));
            ChannelServices.RegisterChannel(channel, true);
            IChatServer obj = (IChatServer)Activator.GetObject(
                typeof(IChatServer),
                "tcp://localhost:8086/IChatServer");
            return true;
        }

		static void Main() {
            System.Console.WriteLine("waiting for user");
            System.Console.ReadLine();
        }
	}
}