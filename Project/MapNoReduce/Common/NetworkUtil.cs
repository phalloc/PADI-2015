using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace PADIMapNoReduce
{
    public class NetworkUtil
    {
        public static int GetFirstAvailablePort(int startRange, int endRange)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            List<int> usedPorts = tcpEndPoints.Select(p => p.Port).ToList<int>();
            
            for (int port = startRange; port < endRange; port++)
            {
                if (!usedPorts.Contains(port))
                {
                    return port;
                }
            }
            return -1;
        }
    }
}
