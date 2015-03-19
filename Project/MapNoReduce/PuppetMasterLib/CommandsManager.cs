using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

using MapNoReduce.Commands;

namespace MapNoReduce
{
    public class CommandsManager
    {
        private static string COMMENT_CHAR = "%";
        private static string CREATE_WORK_PROCESS_CMD = "WORKER";
        private static string SUBMIT_JOB_CMD = "SUBMIT";
        private static string WAIT_CMD = "WAIT";
        private static string STATUS_CMD = "STATUS";
        private static string DELAY_WORKER_CMD = "SLEEPP";
        private static string FREEZE_WORKER_CMD = "FREEZEW";
        private static string UNFREEZE_WORKER_CMD = "UNFREEZEW";
        private static string DISABLE_JOBTRACKER_CMD = "FREEZEC";
        private static string ENABLE_JOBTRACKER_CMD = "UNFREEZEC";

        public List<string> LoadFile(string file)
        {
            List<string> returnValue = new List<string>();
            StreamReader reader = File.OpenText(file);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith(COMMENT_CHAR)){
                    returnValue.Add(parseAndExecute(line));
                }
            }

            reader.Close();
            return returnValue;
        }

        public string parseAndExecute(string line)
        {

            Command c = null;

            if (line.StartsWith(CREATE_WORK_PROCESS_CMD))
            {
                c = new CreateWorkProcessCmd(line);
            }
            else if (line.StartsWith(SUBMIT_JOB_CMD))
            {
                c = new SubmitJobCmd(line);
            }
            else if (line.StartsWith(WAIT_CMD))
            {
                c = new DelayWorkerCmd(line);
            }
            else if (line.StartsWith(STATUS_CMD))
            {
                c = new StatusCmd(line);
            }
            else if (line.StartsWith(DELAY_WORKER_CMD))
            {
                c = new DelayWorkerCmd(line);
            }
            else if (line.StartsWith(FREEZE_WORKER_CMD))
            {
                c = new FreezeWorkerCmd(line);
            }
            else if (line.StartsWith(UNFREEZE_WORKER_CMD))
            {
                c = new UnfreezeWorkerCmd(line);
            }
            else if (line.StartsWith(DISABLE_JOBTRACKER_CMD))
            {
                c = new DisableJobTrackerCmd(line);
            }
            else if (line.StartsWith(ENABLE_JOBTRACKER_CMD))
            {
                c = new EnableJobTrackerCmd(line);
            }

            if (c != null){
                return c.Execute() ? generateSuccessMsg(line) : generateFailureMsg(line);
            }

            return generateFailureMsg("Unknown");
        }

        private string generateSuccessMsg(string line)
        {
            return "[GOOD COMMAND] - " + line;
        }

        private string generateFailureMsg(string line)
        {
            return "[BAD COMMAND] - " + line;
        }
    }


}
