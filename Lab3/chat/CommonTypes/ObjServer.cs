using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingSample
{
    public class ObjServer : MarshalByRefObject, IChatServer
    {
        Dictionary<string, string> dictClients = new Dictionary<string, string>();

        public bool SendMsg(string nick, string message)
        {
            string url = dictClients[nick];

            ObjClient obj = (ObjClient)Activator.GetObject(
                typeof(ObjClient), url);

            if (obj == null)
            {
                System.Console.WriteLine("CAPUT");
                return false;
            }

            System.Console.WriteLine("Sending > " + nick + "@ " + url + " : " + message);

            obj.RecvMsg(message);

            return true;
        }

        public bool Register(string nick, string url)
        {
            System.Console.WriteLine("Registering " + nick + " with url " + url);

            dictClients.Add(nick, url);

            return true;
        }
    }
}
