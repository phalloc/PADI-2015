using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemotingSample {

    public class Server : IChatServer
    {
        
        public bool SendMsg(string nick, string message){
            return true;
        }

        public bool Register(string nick, string url)
        {
            return true;
        }

		static void Main(string[] args) {

			TcpChannel channel = new TcpChannel(8086);
			ChannelServices.RegisterChannel(channel,true);

			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(IChatClient),
				"IChatClient",
				WellKnownObjectMode.Singleton);
      
			System.Console.WriteLine("<enter> para sair...");
			System.Console.ReadLine();
		}
	}
}