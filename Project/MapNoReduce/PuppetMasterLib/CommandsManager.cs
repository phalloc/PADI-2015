using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

using PADIMapNoReduce.Commands;
using System.Threading;

namespace PADIMapNoReduce
{
    public class CommandsManager
    {
        private static string COMMENT_CHAR = "%";

        List<Command> listCommands = new List<Command>();
        List<Command> supportedCommands = new List<Command>();

        PuppetMaster pm = null;

        public CommandsManager(PuppetMaster pm)
        {
            this.pm = pm;
            supportedCommands.Add(new CreateWorkerCmd(pm));
            supportedCommands.Add(new SleepCmd(pm));
            supportedCommands.Add(new FreezeJobTrackerCmd(pm));
            supportedCommands.Add(new UnfreezeJobTrackerCmd(pm));
            supportedCommands.Add(new FreezeWorkerCmd(pm));
            supportedCommands.Add(new WaitCmd(pm));
            supportedCommands.Add(new StatusCmd(pm));
            supportedCommands.Add(new SubmitJobCmd(pm));
            supportedCommands.Add(new UnfreezeWorkerCmd(pm));
        }

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
                        listCommands.Add(ParseCommand(line));
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

        public void ExecuteScript()
        {
            foreach (Command command in listCommands){
                ExecuteCommand(command);
            }

            listCommands.Clear();
        }

        public void ExecuteCommand(string line)
        {
            ParseCommand(line).Execute();      
        }

        private void ExecuteCommand(Command c)
        {
            c.Execute();
        }

        private Command ParseCommand(string line) {
            string commandType = line.Split(' ')[0];

            foreach (Command c in supportedCommands)
            {
                if (c.getCommandName() == commandType)
                {
                    c.Parse(line);
                    return c;
                }
            }

            throw new Exception("No command found");
        }
    }


}
