using System;

using System.Windows.Forms;

namespace RemotingSample
{

    public delegate void AddMessageDel(string msg);

    abstract public class FormRemoteGUI : Form
    {
        abstract public void UpdateForm(string msg);
    }

    public interface IChatClient
    {
        bool RecvMsg(string msg);
    }
}