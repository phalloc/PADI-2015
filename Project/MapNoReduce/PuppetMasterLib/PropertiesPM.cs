using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PADIMapNoReduce
{
    public class PropertiesPM
    {
        public static string workerExeLocation = null;
        public static string clientExeLocation = null;

        public static IDictionary<string,string> ReadDictionaryFile(string fileName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string line in File.ReadAllLines(fileName))
            {
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith("#")) &&
                    (line.Contains('=')))
                {
                    string[] parsed = line.Split('=');

                    string key = parsed[0].Trim();
                    string value = parsed[1].Trim();

                    dictionary.Add(key, value);
                }
            }

            return dictionary;
        }
    }
}
