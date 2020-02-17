using Detectify.ServerDetection.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Provider
{
    public interface IAuthProvider
    {
        Task<AuthResponse> Login(User user);
    }
}
