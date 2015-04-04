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

        public static int GetFirstAvailablePort2(int startRange, int endRange)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            Logger.LogWarn("HEREEEE111111");
            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                Logger.LogWarn("HEREEEE");
                if (tcpi.LocalEndPoint.Port >= startRange && tcpi.LocalEndPoint.Port <= endRange)
                {
                    return tcpi.LocalEndPoint.Port;
                }
            }

            return -1;
        }

        
    }
}
