using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace PADIMapNoReduce
{
    public class Logger
    {

        private static Color WARN_COLOR = Color.Yellow;
        private static Color ERROR_COLOR = Color.Red;
        private static Color INFO_COLOR = Color.Lime;
        private static Color PREFIX_BACK_COLOR = Color.Gainsboro;
        private static Color PREFIX_TEXT_COLOR = Color.Black;        
        private static Color TEXT_BACK_COLOR = Color.Black;
        
        private static FormRemoteGUI form = null;
        private static int numLines = 0;
        public static Logger instance;

        public static void initializeForm(FormRemoteGUI form)
        {
            Logger.form = form;
        }

        public static string generatePrefixString(string type, string filePath, int lineNumber)
        {
            string[] splitedPath = filePath.Split('\\');
            string fileName = splitedPath[splitedPath.Length - 1];
            return String.Format("[{0, 4} - " + DateTime.Now.ToString("HH:mm:ss") + " " + fileName  + ":" + lineNumber + " " + type + "]:", numLines++);
        }

        public static void LogInfo(string msg, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {

            string prefix = generatePrefixString("INFO", filePath, lineNumber);
            msg = "  " + msg;
            
            LogTerminal(prefix + msg);
         
            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.AppendText), new Object[] { PREFIX_TEXT_COLOR, PREFIX_BACK_COLOR, TEXT_BACK_COLOR, INFO_COLOR, prefix, msg });

        }

        public static void LogWarn(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string prefix = generatePrefixString("WARN", filePath, lineNumber);
            msg = "  " + msg;
            LogTerminal(prefix + msg);
            

            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.AppendText), new Object[] { PREFIX_TEXT_COLOR, PREFIX_BACK_COLOR, TEXT_BACK_COLOR, WARN_COLOR, prefix, msg  });
        }

        public static void LogErr(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string prefix = generatePrefixString("ERRO", filePath, lineNumber);
            msg = "  " + msg;
            
            LogTerminal(prefix + msg);
            

            if (form != null)
                form.BeginInvoke(new LogInfoDel(form.AppendText), new Object[] { PREFIX_TEXT_COLOR, PREFIX_BACK_COLOR, TEXT_BACK_COLOR, ERROR_COLOR, prefix, msg  });

        }

        private static void LogTerminal(string msg)
        {
            System.Console.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
