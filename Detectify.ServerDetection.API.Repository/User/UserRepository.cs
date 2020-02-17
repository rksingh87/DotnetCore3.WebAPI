using Detectify.ServerDetection.API.Entities;
using Detectify.ServerDetection.API.Repository.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Repository
{
    public class UserRepository : IUserRepository
    {

        public UserRepository()
        {

        }
        public async Task<User> GetUser(string userName, string password)
        {
            using var mg = new MongoDatabaseContext(AppSettings.ConnectionString_MongoDB, AppSettings.MongoDB_ServerDetectionDatabase);
            var builder = new FilterDefinitionBuilder<User>();
            var filter = builder.Eq(t => t.UserName, userName) & builder.Eq(t => t.Password, password);
            return await mg.GetFirstDocumentAsync<User>(AppSettings.MongoDB_UserCollection, filter);
        }
    }
}
