using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;

namespace RemotingSample {

	public class Client : MarshalByRefObject, IChatClient{

        public bool RecvMsg(string msg)
        {
            //implementacao fixe aqui
            return true;
        }

        public bool SendMessage(string msg)
        {
            //cenas
            return true;
        }

        public bool Register(string nick, string port)
        {
            //cenas
            return true;
        }

		static void Main() {
			TcpChannel channel = new TcpChannel();
			ChannelServices.RegisterChannel(channel,true);

			IChatServer obj = (IChatServer) Activator.GetObject(
				typeof(IChatServer),
				"tcp://localhost:8086/IChatServer");

	 		try
	 		{
	 			//
	 		}
	 		catch(SocketException)
	 		{
	 			System.Console.WriteLine("Could not locate server");
	 		}
            
			Console.ReadLine();
		}
	}
}