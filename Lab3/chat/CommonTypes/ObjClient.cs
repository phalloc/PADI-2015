using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingSample
{
    public class ObjClient : MarshalByRefObject, IChatClient
    {
        public bool RecvMsg(string msg)
        {
            //write in console
            System.Console.WriteLine("RECEIVED FROM SERVER: " + msg);

            //write in form
            //msgDel(msg);
            return true;
        }
    }
}
