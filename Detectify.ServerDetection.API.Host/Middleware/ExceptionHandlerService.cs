using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Web.Middleware
{

    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionHandlerService
    {
        /// <summary>
        /// Configure Exception Handler
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionHandlerService(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        await context.Response.WriteAsync(new
                        {
                            Status = false,
                            StatusCode = context.Response.StatusCode.ToString(),
                            Error = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
