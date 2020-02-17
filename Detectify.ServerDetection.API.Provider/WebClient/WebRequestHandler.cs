using Detectify.ServerDetection.API.Entities;
using Detectify.ServerDetection.API.Entities.Common;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Provider
{
    public class WebRequestHandler : IWebRequestHandler
    {
        public async Task<DnsDetail> GetDnsDetailAsync(string hostName)
        {
            DnsDetail dnsDetail = null;
            try
            {
                WebRequest request = WebRequest.Create(string.Format("https://{0}", hostName));
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                if (response != null && response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    string serverName = response.GetResponseHeader(AppConstant.SERVER);
                    dnsDetail = new DnsDetail
                    {
                        Name = hostName,
                        WebServer = string.IsNullOrEmpty(serverName) ? string.Empty : serverName,
                        IPAddresses = await this.GetIpAddressesAsync(hostName)
                    };
                }
            }
            catch
            {

            }
            return dnsDetail;
        }


        private async Task<string[]> GetIpAddressesAsync(string hostName)
        {
            IPAddress[] ipaddress = await Dns.GetHostAddressesAsync(hostName);
            return ipaddress.Where(t => t.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(t => t.ToString()).ToArray();
        }
    }
}
