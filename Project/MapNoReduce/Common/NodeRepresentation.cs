using System.Collections;
using System.Collections.Generic;
using System.Reflection;
namespace PADIMapNoReduce
{
    public class NodeRepresentation
    {

        public static string ID = "ID";
        public static string SERVICE_URL = "SERVICE_URL";
        public static string NEXT_URL = "NEXT_URL";
        public static string NEXT_NEXT_URL = "NEXT_NEXT_URL";

        public IDictionary<string, string> info;

        public NodeRepresentation(string id, string serviceUrl)
        {
            info = new Dictionary<string, string>();

            info.Add(ID, id);
            info.Add(SERVICE_URL, serviceUrl);
        }

        public NodeRepresentation(IDictionary<string, string> info)
        {
            this.info = info;
        }
    }
}
