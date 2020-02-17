using Detectify.ServerDetection.API.Entities;
using Detectify.ServerDetection.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Detectify.ServerDetection.API.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IUserRepository userRepository;

        public AuthProvider(IUserRepository _userRepository)
        {
            this.userRepository = _userRepository;
        }

        public async Task<AuthResponse> Login(User user)
        {
            if (ValidateUserCredential(user))
            {
                User userObj = await this.userRepository.GetUser(user.UserName, user.Password);
                if (userObj == null)
                    throw new Exception("Invalid UserName/Password");
                else
                {
                    return new AuthResponse()
                    {
                        Token = this.GenerateToken(userObj),
                        TokenType = JwtBearerDefaults.AuthenticationScheme,
                        UserInfo = userObj.GetUserWithoutPassword()
                    };
                }
            }
            else
            {
                throw new Exception("UserName/Password is required.");
            }
        }

        private bool ValidateUserCredential(User user)
        {
            return (user != null && !string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password));
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.JWT_SymmetricSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                }),
                Issuer = AppSettings.JWT_Issuer,
                Audience = AppSettings.JWT_Audience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
