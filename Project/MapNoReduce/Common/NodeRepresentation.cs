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
        public static string CURRENT_JT = "CURRENT_JT";

        public static string PROCESSED_SPLITS = "PROCESSED_SPLITS";

        public IDictionary<string, string> info;

        public NodeRepresentation()
        {
            info = new Dictionary<string, string>();

            this.info.Add(ID, "<NO_ID>");
            this.info.Add(SERVICE_URL, "<NO_SERVICE_URL>");
        }

        public NodeRepresentation(IDictionary<string, string> info)
        {
            this.info = info;

            if (!this.info.ContainsKey(ID))
            {
                this.info.Add(ID, "<NO_ID>");
            }

            if (!this.info.ContainsKey(SERVICE_URL))
            {
                this.info.Add(SERVICE_URL, "<NO_SERVICE_URL>");
            }
        }
    }
}
