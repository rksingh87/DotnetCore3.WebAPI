using Detectify.ServerDetection.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Provider
{
    public class DnsDetailProvider : IDnsDetailProvider
    {
        private readonly ICacheManager cacheManager;
        private readonly IWebRequestHandler webRequestHandler;

        public DnsDetailProvider(ICacheManager _cacheManager, IWebRequestHandler _webRequestHandler)
        {
            this.cacheManager = _cacheManager;
            this.webRequestHandler = _webRequestHandler;
        }


        public async Task<List<DnsDetail>> GetDnsDetailsAsync(string[] dnsList)
        {
            List<DnsDetail> dnsDetails = new List<DnsDetail>();

            var taskList = dnsList.Select(async hName =>
            {
                try
                {

                    if (this.ValidateDns(hName))
                    {
                        hName = hName.Trim().ToLower();

                        var dnsDetailsCached = this.GetDnsDetailFromCache(hName);
                        if (dnsDetailsCached != null)
                        {
                            dnsDetails.Add(dnsDetailsCached);
                        }
                        else
                        {
                            DnsDetail dnsDetail = await this.webRequestHandler.GetDnsDetailAsync(hName);
                            if (dnsDetail != null)
                            {
                                dnsDetails.Add(dnsDetail);
                            }
                        }
                    }
                }
                finally
                {

                }
            });

            await Task.WhenAll(taskList);
            return dnsDetails;
        }


        private bool ValidateDns(string dnsName)
        {
            return dnsName != null && Uri.CheckHostName(dnsName).Equals(UriHostNameType.Dns);
        }

        private DnsDetail GetDnsDetailFromCache(string key)
        {
            return this.cacheManager.Get<DnsDetail>(key);
        }
    }
}
