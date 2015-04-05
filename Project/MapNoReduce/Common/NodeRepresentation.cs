using System.Collections;
using System.Collections.Generic;

namespace PADIMapNoReduce
{
    public class NodeRepresentation
    {
        public string id;
        public string myUrl;
        public string nextUrl;
        public string nextNextUrl;
        public string currentRole;
        public string status;
        public string currentJobTracker;

        public NodeRepresentation(string id, string myUrl)
        {
            this.id = id;
            this.myUrl = myUrl;
        }
        

        public NodeRepresentation(string id, string myUrl, string nextUrl, string nextNextUrl, string currentRole, string status, string currentJobTracker)
        {
            this.id = id;
            this.myUrl = myUrl;
            this.nextUrl = nextUrl;
            this.nextNextUrl = nextNextUrl;
            this.currentRole = currentRole;
            this.status = status;
            this.currentJobTracker = currentJobTracker;
        }

        public static NodeRepresentation ConvertFromNodeStatus(IDictionary<string, string> dic){
            string id;
            string myUrl;
            string nextUrl;
            string nextNextUrl;
            string currentRole;
            string status;
            string currentJobTracker;

            dic.TryGetValue("ID", out id);
            dic.TryGetValue("myURL", out myUrl);
            dic.TryGetValue("NextUrl", out nextUrl);
            dic.TryGetValue("NextNextUrl", out nextNextUrl);
            dic.TryGetValue("currentRole", out currentRole);
            dic.TryGetValue("status", out status);
            dic.TryGetValue("CurrentJobTracker", out currentJobTracker);

            return new NodeRepresentation(id, myUrl, nextUrl, nextNextUrl, currentRole, status, currentJobTracker);
        }

        public string Print()
        {
           return "id: " + this.id
                + "\r\n" + "nextUrl: " + this.nextUrl
                + "\r\n" + "nextNextUrl: " + this.nextNextUrl
                + "\r\n" + "currentRole: " + this.currentRole
                + "\r\n" + "status: " + this.status
                + "\r\n" + "currentJobTracker: " + this.currentJobTracker;
        }
    }
}
