using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class Logger
    {
        private static FormRemoteGUI form = null;
        private static int numLines = 0;
        public static Logger instance;

        public static void initializeForm(FormRemoteGUI form)
        {
            Logger.form = form;
        }

        public static void LogInfo(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogTerminal("INFO", s);
            }

            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.LogInfo), new Object[] { listS });

        }

        public static void LogWarn(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogTerminal("WARN", s);
            }

            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.LogWarn), new Object[] { listS });

        }

        public static void LogErr(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogTerminal("ERR", s);
            }

            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.LogErr), new Object[] { listS });
            else
                LogTerminal("WARN", "Form not initialized");
        }

        public static void LogInfo(string msg)
        {
            LogTerminal("INFO", msg);
            
            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.LogInfo), new Object[] { msg });

        }

        public static void LogWarn(string msg)
        {
            LogTerminal("WARN", msg);

            if (form != null)
                form.BeginInvoke(new LogWarnDel(form.LogWarn), new Object[] { msg });

        }

        public static void LogErr(string msg)
        {
            LogTerminal("ERR", msg);

            if (form != null)
                form.BeginInvoke(new LogErrDel(form.LogErr), new Object[] { msg });

        }

        private static void LogTerminal(string prefix, string msg)
        {
            string formatString = String.Format("[{0, 4} - " + DateTime.Now.ToString("HH:mm:ss") + "] :", numLines++);
            formatString += "[" + prefix + "]: " + msg;
            
            System.Console.WriteLine(formatString);
            System.Diagnostics.Debug.WriteLine(formatString);
        }
    }
}
