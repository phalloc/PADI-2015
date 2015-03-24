using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemotingSample
{
    public class ObjServer : MarshalByRefObject, IChatServer
    {
        Dictionary<string, string> dictClients = new Dictionary<string, string>();
        
        public bool SendMsg(string nick, string message)
        {

            foreach (KeyValuePair<string, string> entry in dictClients)
            {
                if (nick == entry.Key)
                {
                    continue;
                }

                ObjClient obj = (ObjClient)Activator.GetObject(
                    typeof(ObjClient), entry.Value);

                if (obj == null)
                {
                    return false;
                }

                System.Console.WriteLine("Sending to " + entry.Key + " : " + message);

                obj.RecvMsg(nick + "> " + message);
            }

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
