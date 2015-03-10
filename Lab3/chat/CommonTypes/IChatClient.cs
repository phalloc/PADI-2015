using System;

namespace RemotingSample
{
	public interface IChatClient{

        bool RecvMsg(string msg);
  }
}