using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegistrationSystem.AccessData.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RegistrationSystemDb")));


            //services.AddScoped<IAccountsRepository, AccountsRepository>( );
            //services.AddScoped<INotesRepository, NotesRepository>( );
            //services.AddScoped<IFilesRepository, FilesRepository>( );
            return services;
        }
    }
}
