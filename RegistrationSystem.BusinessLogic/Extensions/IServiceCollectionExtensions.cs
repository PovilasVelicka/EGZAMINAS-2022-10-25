using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.BusinessLogic.Services.AuthServices;

using System.Text;

namespace RegistrationSystem.BusinessLogic.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorization (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                });

            services.AddAuthorizationCore( );

            services.AddScoped<IJwtService, JwtService>( );

            return services;
        }

        public static IServiceCollection AddServices (this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>( );        
            return services;
        }
    }
}
