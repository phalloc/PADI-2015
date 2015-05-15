using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace PADIMapNoReduce
{
    partial class Node
    {
        private object mapper = null;
        private Type mapperType = null;

        private UTF8Encoding encoding = new UTF8Encoding();
        private string mapperName;
        private byte[] mapperCode;

        private void fetchItProcessItSendIt(long startSplit, long endSplit, long splitId)
        {


            int maxMemoryGet = 5242880;
            long firstIndex = startSplit;
            long endIndex = startSplit;

            byte[] gradualSplit;
            //byte[] mySplitBytes = new byte[endSplit - startSplit];

            IList<KeyValuePair<string, string>> processedWork;
            bool firstReturn = true;



            while (endIndex < endSplit)
            {

                
                if (!currentJobTracker.CanContinueProcessSplit(id, splitId))
                {
                    Logger.LogErr("ABORT SPLIT PROCESS!!!!!!!!!!!!!!!!!!!!!!");
                    return;
                }

                endIndex = firstIndex + maxMemoryGet;

                if (endIndex > endSplit)
                {
                    endIndex = endSplit;
                }

                //Logger.LogInfo("Fetching (" + firstIndex + ", " + endIndex + ")");

                gradualSplit = client.getWorkSplit(firstIndex, endIndex);
                //if(gradualSplit == null)
                //  Logger.LogInfo("EMPTY RETURN");

                processedWork = ProcessSplit(gradualSplit);


                client.returnWorkSplit(processedWork, splitId, firstReturn);
                firstIndex += gradualSplit.Length;
                firstReturn = false;
            }


            //byte[] gradualSplit = client.getWorkSplit(startSplit, endSplit);

        }

        private IList<KeyValuePair<string, string>> ProcessSplit(byte[] splitBytes)
        {
            List<KeyValuePair<string, string>> processedWork = new List<KeyValuePair<string, string>>();

            if (splitBytes == null)
                return processedWork;

            int normalInterval = 55;
            int byteIndex = 0;
            int stringIndex = -1;
            int intervalToRead = normalInterval;//valor arbitrario para o comprimento normal de uma linha
            byte[] intervalBytes = new byte[intervalToRead];
            StringBuilder builder = new StringBuilder();
            int maxIndex = splitBytes.Length - 1;
            string intervalString;
            while (byteIndex < maxIndex)
            {

                //this prevents garbage bytes from being converted to string
                if (intervalToRead < normalInterval)
                    intervalBytes = new byte[intervalToRead];

                Array.Copy(splitBytes, byteIndex, intervalBytes, 0, intervalToRead);

                intervalString = encoding.GetString(intervalBytes);
                stringIndex = intervalString.IndexOf('\n');



                if (stringIndex == -1)
                {
                    builder.Append(intervalString);
                    byteIndex += intervalToRead;
                }
                else
                {
                    builder.Append(intervalString.Substring(0, stringIndex));
                    processedWork.AddRange(processLineWithMapper(builder.ToString()));
                    builder.Clear();
                    byteIndex += stringIndex + 1;
                }

                if (intervalToRead + byteIndex > maxIndex)
                {
                    intervalToRead = maxIndex - byteIndex;
                }


            }


            string lastString = builder.ToString();
            if (lastString != "")
            {
                processedWork.AddRange(processLineWithMapper(lastString));
            }
            //Logger.LogInfo("Processed Gradual split");
            //Logger.LogInfo("Length Work: "+ processedWork.Count);

            return processedWork;
        }

        private IList<KeyValuePair<string, string>> processLineWithMapper(string line)
        {


            // Dynamically Invoke the method
            object[] args = new object[] { line };
            object resultObject = this.mapperType.InvokeMember("Map",
              BindingFlags.Default | BindingFlags.InvokeMethod,
                   null,
                   this.mapper,
                   args);
            IList<KeyValuePair<string, string>> result = (IList<KeyValuePair<string, string>>)resultObject;

            //DEBUG purposes
            /*                         
             Logger.LogInfo("Processed: ");

             foreach (KeyValuePair<string, string> p in result)
            {
                Logger.LogInfo("key: " + p.Key + ", value: " + p.Value);
            }
            */
            return result;
        }

        private void GetMapperObject(string mapperName, byte[] mapperCode)
        {
            Assembly assembly = Assembly.Load(mapperCode);
            //procurar o metodo
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    if (type.FullName.EndsWith("." + mapperName))
                    {
                        // create an instance of the object
                        this.mapper = Activator.CreateInstance(type);
                        this.mapperType = type;
                        return;
                    }
                }
            }
            throw (new System.Exception("could not invoke method"));
        }
    }
}
