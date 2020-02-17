using Detectify.ServerDetection.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Provider
{
    public interface IDnsDetailProvider
    {
        Task<List<DnsDetail>> GetDnsDetailsAsync(string[] dnsList);
    }
}
