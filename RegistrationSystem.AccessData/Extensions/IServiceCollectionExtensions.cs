using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces;

namespace RegistrationSystem.AccessData.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RegistrationSystemDb")));

            services.AddScoped<IAccountsRepository, AccountsRepository>( );
            services.AddScoped<IAddressesRepository, AddressesRepository>( );

            return services;
        }
    }
}
