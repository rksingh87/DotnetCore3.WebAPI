using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detectify.ServerDetection.API.Entities
{
    public class AppSettings
    {
        public static string ConnectionString_MongoDB { get; private set; }
        public static string ConnectionString_Redis { get; private set; }

        public static string MongoDB_ServerDetectionDatabase { get; private set; }

        public static string MongoDB_UserCollection { get; private set; }

        public static string JWT_SymmetricSecurityKey { get; private set; }

        public static string JWT_Issuer { get; private set; }

        public static string JWT_Audience { get; private set; }


        public static void SetConfiguration(IConfiguration configuration)
        {
            ConnectionString_MongoDB = configuration.GetConnectionString("MongoDB");
            ConnectionString_Redis = configuration.GetConnectionString("Redis");

            MongoDB_ServerDetectionDatabase = configuration["MongoDB:ServerDetectionDatabase"];
            MongoDB_UserCollection = configuration["MongoDB:UserCollection"];

            JWT_SymmetricSecurityKey = configuration["JWT:SymmetricSecurityKey"];
            JWT_Issuer = configuration["JWT:Issuer"];
            JWT_Audience = configuration["JWT:Audience"];
        }



    }
}
