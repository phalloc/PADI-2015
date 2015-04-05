using System.Collections;
using System.Collections.Generic;

namespace PADIMapNoReduce
{
    class NodeRepresentation
    {
        string id;
        string myUrl;
        string nextUrl;
        string nextNextUrl;
        string currentRole;
        string status;
        string currentJobTracker;

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


        public void SetMyUrl(string myUrl)
        {
            this.myUrl = myUrl;
        }

        public string GetMyUrl()
        {
            return this.myUrl;
        }

        public void SetNextUrl(string nextUrl){
            this.nextUrl = nextUrl;
        }

        public void SetNextNextUrl(string nextNextUrl)
        {
            this.nextNextUrl = nextNextUrl;
        }

        public void SetCurrentRole(string currentRole)
        {
            this.currentRole = currentRole;
        }

        public void SetStatus(string status)
        {
            this.status = status;
        }

        public string GetId()
        {
            return id;
        }

        public string GetNextUrl()
        {
            return nextUrl;
        }

        public string GetNextNextUrl()
        {
            return nextNextUrl;
        }

        public string GetType()
        {
            return currentRole;
        }

        public string GetStatus()
        {
            return status;
        }

        public string GetCurrentJobTracker()
        {
            return this.currentJobTracker;
        }

        public void SetCurrentJobTracker(string currentJobTracker)
        {
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
            dic.TryGetValue("myUrl", out myUrl);
            dic.TryGetValue("NextUrl", out nextUrl);
            dic.TryGetValue("NextNextUrl", out nextNextUrl);
            dic.TryGetValue("currentRole", out currentRole);
            dic.TryGetValue("status", out status);
            dic.TryGetValue("CurrentJobTracker", out currentJobTracker);

            return new NodeRepresentation(id, myUrl, nextUrl, nextNextUrl, currentRole, status, currentJobTracker);
        }

        public string ToString()
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
