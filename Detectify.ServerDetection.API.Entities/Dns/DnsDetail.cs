using System;
using System.Collections.Generic;
using System.Text;

namespace Detectify.ServerDetection.API.Entities
{
    public class DnsDetail
    {
        public string Name { get; set; }

        public string WebServer { get; set; }

        public string[] IPAddresses { get; set; }
    }
}
