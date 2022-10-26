using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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
            services.AddScoped<IAuthService, AuthService>( );
            services.AddScoped<IJwtService, JwtService>( );

            return services;
        }

        //public static IServiceCollection AddServices (this IServiceCollection services)
        //{
        //    services.AddScoped<IPeopleAdminService, PeopleAdminService>( );
        //    services.AddScoped<INotesService, NotesService>( );
        //    return services;
        //}
    }
}
