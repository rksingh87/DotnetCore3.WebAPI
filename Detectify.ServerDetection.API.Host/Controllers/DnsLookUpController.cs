using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detectify.ServerDetection.API.Entities;
using Detectify.ServerDetection.API.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detectify.ServerDetection.API.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/dns/lookup")]
    [ApiController]
    public class DnsLookUpController : ControllerBase
    {

        private readonly IDnsDetailProvider dnsDetailProvider;
        private readonly ICacheManager cacheManager;
        /// <summary>
        /// Contructor
        /// </summary>
        public DnsLookUpController(IDnsDetailProvider _dnsDetailProvider, ICacheManager _cacheManager)
        {
            this.dnsDetailProvider = _dnsDetailProvider;
            this.cacheManager = _cacheManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servername"></param>
        /// <param name="dnsList"></param>
        /// <returns></returns>
        //[HttpGet]
        [HttpPost("{servername}")]
        public async Task<Dictionary<string, string[]>> GetDnsLookup([FromRoute] string servername, [FromBody] string[] dnsList)
        {
            Dictionary<string, string[]> response = new Dictionary<string, string[]>();

            List<DnsDetail> dnsDetails = await this.dnsDetailProvider.GetDnsDetailsAsync(dnsList);
            var selectedWebServers = dnsDetails.Where(t => t.WebServer.Contains(servername)).ToList();
            selectedWebServers.ForEach(k =>
            {
                this.cacheManager.Put<DnsDetail>(k.Name, k);
                response.Add(k.Name, k.IPAddresses);
            });

            return response;
        }
    }
}