using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Web.Middleware
{


    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerService
    {
        /// <summary>
        /// Swagger Configure Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerServiceConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerDetection API", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(filePath);
            });
        }


        /// <summary>
        /// Swagger Configuration For Application Builder
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/swagger.json", "Detectify.ServerDetection.API.Web");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
