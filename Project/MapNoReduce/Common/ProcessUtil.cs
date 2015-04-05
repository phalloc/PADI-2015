using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PADIMapNoReduce
{
    public class ProcessUtil
    {
        public static void ExecuteNewProcess(string fullPath, string arguments)
        {
            string[] parsedDirectory = fullPath.Split('\\');
            string fileName = parsedDirectory[parsedDirectory.Length - 1];

            string directory = "";
            for (int i = 0; i < parsedDirectory.Length - 1; i++)
            {
                directory += parsedDirectory[i] + "\\";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo(fileName);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.Arguments = arguments;
            startInfo.WorkingDirectory = directory;
            try { 
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }
        }
    }
}