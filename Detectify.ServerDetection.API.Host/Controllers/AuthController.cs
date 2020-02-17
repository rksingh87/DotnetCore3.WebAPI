using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detectify.ServerDetection.API.Entities;
using Detectify.ServerDetection.API.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detectify.ServerDetection.API.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthProvider authProvider;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_authProvider"></param>
        public AuthController(IAuthProvider _authProvider)
        {
            authProvider = _authProvider;
        }

        
        /// <summary>
        /// Gets the Token By User Credential
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthResponse> Login([FromBody] User user)
        {
            return await this.authProvider.Login(user);
        }
    }
}