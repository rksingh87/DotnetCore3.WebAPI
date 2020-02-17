using Detectify.ServerDetection.API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Web.Middleware
{

    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticationService
    {
        /// <summary>
        /// Add Auth0 Service Configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthenticationServiceConfiguration(this IServiceCollection services)
        {

            var key = Encoding.ASCII.GetBytes(AppSettings.JWT_SymmetricSecurityKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AppSettings.JWT_Issuer,
                    ValidAudience = AppSettings.JWT_Audience,
                };
            });
        }
    }
}
