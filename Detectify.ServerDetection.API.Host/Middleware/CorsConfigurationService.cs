using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Web.Middleware
{

    /// <summary>
    /// 
    /// </summary>
    public static class CorsConfigurationService
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ALLOW_ALL_ORIGINS_POLICY = "ALLOW_ALL_ORIGINS_POLICY";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors((option) =>
            {
                option.AddPolicy(ALLOW_ALL_ORIGINS_POLICY,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
        }
    }
}
