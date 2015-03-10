using System;
using System.Collections.Generic;
using System.Text;

namespace RemotingSample
{
    public interface IChatServer
    {
        bool SendMsg(string nick, string message);
        bool Register(string nick, string url);
    }
}
