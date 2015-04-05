﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class FreezeWorkerCmd : Command
    {

        public static string COMMAND = "FREEZEW";
        
        string workerId;


        public FreezeWorkerCmd(PuppetMaster pm) : base(pm) { }


        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                workerId = args[1];

                return true;
            }

            return false;

        }

        protected override void ExecuteAux()
        {
             FreezeWorker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }


        public void FreezeWorker(string workerId)
        {
           
            try
            {
                Logger.LogInfo("[FREEZING] " + workerId);
                IWorker w = puppetMaster.GetRemoteWorker(workerId);
                w.FreezeWorker();
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }

        }
    }
}
