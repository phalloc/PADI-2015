using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Net.Sockets;

namespace RemotingSample {

    public delegate void MsgDelegate(string msg);

	public class Client : MarshalByRefObject, IChatClient{

        private TcpChannel channel = null;
        private IChatServer obj = null;
        private string nick = null;

        MsgDelegate msgDel;

        public bool RecvMsg(string msg)
        {
            //write in console
            System.Console.WriteLine(msg);

            //write in form
            msgDel(msg);
            return true;
        }

        public void handler_writeInForm(string msg)
        {
            //updates the form textbox               
        }

        public bool SendMessage(string msg)
        {
            obj.SendMsg(nick, msg);
            return true;
        }

        public bool Register(string nick, string port)
        {
            msgDel = new MsgDelegate(handler_writeInForm);
            System.Console.WriteLine("registering");
            this.nick = nick;
            channel = new TcpChannel(Convert.ToInt32(port));
            ChannelServices.RegisterChannel(channel, true); //NO EXEMPLO DO PROF ESTAVA A FALSE http://groups.ist.utl.pt/meic-padi/labs/aula3/aula3-slides.pdf

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