using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace MapNoReduce
{

    //TODO limpar/generalizar o codigo e passar cada comando para uma classe que implementa o parse e o execute

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


        private string generateSuccessMsg(string line)
        {
            return "[GOOD COMMAND] - " + line;
        }

        private string generateFailureMsg(string line)
        {
            return "[BAD COMMAND] - " + line;
        }

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
            if (line.StartsWith(CREATE_WORK_PROCESS_CMD))
            {
                return ParseAndExectuteCreateWorkProcess(line);
            }
            else if (line.StartsWith(SUBMIT_JOB_CMD))
            {
                return ParseAndExectuteSubmitJob(line);
            }
            else if (line.StartsWith(WAIT_CMD))
            {
                return ParseAndExectuteSleep(line);
            }
            else if (line.StartsWith(STATUS_CMD))
            {
                return ParseAndExecuteStatus(line);
            }
            else if (line.StartsWith(DELAY_WORKER_CMD))
            {
                return ParseAndExectuteDelayWorker(line);
            }
            else if (line.StartsWith(FREEZE_WORKER_CMD))
            {
                return ParseAndExectuteFreezeWorker(line);
            }
            else if (line.StartsWith(UNFREEZE_WORKER_CMD))
            {
                return ParseAndExectuteUnfreezeWorker(line);
            }
            else if (line.StartsWith(DISABLE_JOBTRACKER_CMD))
            {
                return ParseAndExectuteDisableJobTracker(line);
            }
            else if (line.StartsWith(ENABLE_JOBTRACKER_CMD))
            {
                return ParseAndExectuteEnableJobTracker(line);
            }

            return "BAD INPUT: " + line;
        }

        private string ParseAndExecuteStatus(string line)
        {
            RefreshStatus();
            return generateSuccessMsg(line);
        }

        private string ParseAndExectuteSubmitJob(string line) {
            string[] args = line.Split(' ');
            if (args.Length == 6)
            {
                string entryUrl = args[1];
                string inputFile = args[2];
                string outputFile = args[3];
                int numSplits = Convert.ToInt32(args[4]);
                string className = args[5];

                //COMMON = DLL where the className is at
                SubmitJob(entryUrl, inputFile, outputFile, numSplits, Type.GetType(className + ",Common"));

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        private string ParseAndExectuteCreateWorkProcess(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 5)
            {
                string id = args[1];
                string puppetMasterUrl = args[2];
                string serviceUrl = args[3];
                string entryUrl = args[4];

                //COMMON = DLL where the className is at
                CreateWorkProcess(id, puppetMasterUrl, serviceUrl, entryUrl);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        private string ParseAndExectuteSleep(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                int sec = Convert.ToInt32(args[1]);

                Sleep(sec);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line); 
        }

        private string ParseAndExectuteFreezeWorker(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                string workerId = args[1];

                FreezeWorker(workerId);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        private string ParseAndExectuteUnfreezeWorker(string line)
        {

            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                string workerId = args[1];

                UnfreezeWorker(workerId);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        private string ParseAndExectuteEnableJobTracker(string line)
        {

            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                string workerId = args[1];

                EnableJobTracker(workerId);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        private string ParseAndExectuteDisableJobTracker(string line)
        {

            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                string workerId = args[1];

                DisableJobTracker(workerId);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        public bool Sleep(int seconds)
        {
            System.Diagnostics.Debug.WriteLine("Sleep");
            return true;
        }

        public bool CreateWorkProcess(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {
            System.Diagnostics.Debug.WriteLine("CreateWorkProcess");
            return true;
        }

        public bool RefreshStatus()
        {
            System.Diagnostics.Debug.WriteLine("refreshStatus");

            return true;
        }

        public bool SubmitJob(string entryUrl, string inputFile, string outputFile, int numSplits, Type map) {

            var instance = Activator.CreateInstance(map);
            IMap mapper = (IMap) instance;

            System.Diagnostics.Debug.WriteLine("HERE -> " + mapper.Map("this sentence is now uppercase"));

            return true;
        }

        private string ParseAndExectuteDelayWorker(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                string workerId = args[1];
                int sec = Convert.ToInt32(args[2]);

                DelayWorkerProcess(workerId, sec);

                return generateSuccessMsg(line);
            }

            return generateFailureMsg(line);
        }

        public bool DelayWorkerProcess(string workerId, int delay)
        {
            System.Diagnostics.Debug.WriteLine("DelayWorkerProcess");
            return true;
        }

        public bool FreezeWorker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("FreezeWorker");
            return true;
        }

        public bool UnfreezeWorker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("UnfreezeWorker");
            return false;
        }

        public bool DisableJobTracker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("DisableJobTracker");
            return false;
        }

        public bool EnableJobTracker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("EnableJobTracker");        
            return false;
        }

    }


}
