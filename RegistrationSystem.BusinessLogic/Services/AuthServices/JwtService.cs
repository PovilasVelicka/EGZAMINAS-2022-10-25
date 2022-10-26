using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using RegistrationSystem.Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistrationSystem.BusinessLogic.Services.AuthServices
{
    internal class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetJwtToken (Account account)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.LoginName),
                new Claim(ClaimTypes.Role, account.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
            };

            var secret = _configuration.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                  issuer: _configuration.GetSection("Jwt:Issuer").Value,
                  audience: _configuration.GetSection("Jwt:Audience").Value,
                  claims: claims,
                  expires: DateTime.Now.AddDays(1),
                  signingCredentials: cred
            );

            return new JwtSecurityTokenHandler( ).WriteToken(token);
        }
    }
}
