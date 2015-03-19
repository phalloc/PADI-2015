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
        public static string CREATE_WORK_PROCESS_CMD = "WORKER";
        public static string SUBMIT_JOB_CMD = "SUBMIT";
        public static string WAIT_CMD = "WAIT";
        public static string STATUS_CMD = "STATUS";
        public static string DELAY_WORKER_CMD = "SLEEPP";
        public static string FREEZE_WORKER_CMD = "FREEZEW";
        public static string UNFREEZE_WORKER_CMD = "UNFREEZEW";
        public static string DISABLE_JOBTRACKER_CMD = "FREEZEC";
        public static string ENABLE_JOBTRACKER_CMD = "UNFREEZEC";

        List<Command> listCommands = null;

        public void LoadFile(string file)
        {
            StreamReader reader = null;
            try
            {
                listCommands = new List<Command>();
                reader = File.OpenText(file);

                string line;
                int numLines = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith(COMMENT_CHAR))
                    {
                        Command c = ParseCommand(line);
                        if (c == null)
                        {
                            throw new Exception("Error parsing command at line " + numLines);
                        }
                        listCommands.Add(c);
                    }

                    numLines++;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

            }
        }

        public List<string> ExecuteScript()
        {
            List<string> returnValue = new List<string>();

            foreach (Command command in listCommands){
                returnValue.Add(ExecuteCommand(command));
            }

            listCommands = new List<Command>();

            return returnValue;
        }

        public string ExecuteCommand(Command c)
        {
            c.Parse();
            c.Execute();
            return c.getResult();            
        }

        public Command ParseCommand(string line) {
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
                c = new SleepCmd(line);
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
            else {
                throw new Exception("Invalid command: " + line);
            }

            c.Parse();

            return c;

        }
    }


}
