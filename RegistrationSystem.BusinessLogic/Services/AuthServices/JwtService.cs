using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteBook.Common.Interfaces.Services;
using NoteBook.Entity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteBook.BusinessLogic.Services.AuthServices
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
            // Galima prideti daugiau pav. Id...
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.User.FullName),
                new Claim(ClaimTypes.Email, account.Email.Value),
                new Claim(ClaimTypes.Role, account.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.UserId?.ToString() ??  Guid.Empty.ToString())
            };

            var secret = _configuration.GetSection("Jwt:Key").Value;
            // secret reikia  prisiskirti simetric securyti objektui
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
