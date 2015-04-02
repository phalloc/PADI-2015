using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class Logger
    {
        private FormRemoteGUI form = null;
        private int numLines = 0;

        public Logger(FormRemoteGUI form)
        {
            this.form = form;
        }

        public void LogInfo(string msg)
        {
            LogTerminal("INFO", msg);
            form.BeginInvoke(new LogInfoDel(form.LogInfo), new Object[] { msg });
        }

        public void LogWarn(string msg)
        {
            LogTerminal("WARN", msg);
            form.BeginInvoke(new LogWarnDel(form.LogWarn), new Object[] { msg });
        }

        public void LogErr(string msg)
        {
            LogTerminal("ERR", msg);
            form.BeginInvoke(new LogErrDel(form.LogErr), new Object[] { msg });
        }

        private void LogTerminal(string prefix, string msg)
        {
            string formatString = String.Format("[{0, 4} - " + DateTime.Now.ToString("HH:mm:ss") + "] :", numLines++);
            formatString += "[" + prefix + "]: " + msg;
            
            System.Console.WriteLine(formatString);
            System.Diagnostics.Debug.WriteLine(formatString);
        }
    }
}
