using PADIMapNoReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 6)
            {
                Logger.LogErr("Error: Invalid arguments. Usage: [required: entryUrl, jobFilePath, destPath, splits, mapperName, mapperPath])");
                Logger.LogInfo("Press any key to quit");
                Console.ReadKey();
                return;
            }

            Client client = new Client();

            string entryURL = args[0];
            string jobFilePath = args[1];
            string destinationPath = args[2];
            int splits = Int32.Parse(args[3]);
            string mapperName = args[4];
            string mapperPath = args[5];

            Logger.LogInfo("Submitting job " + jobFilePath + " to " + entryURL + " with " + splits + "splits with mapper " + mapperName + " at " + mapperPath + " and writing results to " + destinationPath);
            try { 
                client.submitJob(jobFilePath, destinationPath, entryURL, splits, mapperName, mapperPath);
            }
            catch (Exception ex)
            {
                Logger.LogErr("Error during submit: " + ex.Message);
            }

            Logger.LogInfo("Press any key to quit");
            Console.ReadLine();

        }
    }
}
