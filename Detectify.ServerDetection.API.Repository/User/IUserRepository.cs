using Detectify.ServerDetection.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userName, string password);
    }
}
