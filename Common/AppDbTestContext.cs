using Microsoft.EntityFrameworkCore;
using RegistrationSystem.AccessData;

namespace Common
{
    internal class AppDbTestContext : DbContext
    {
        private readonly AppDbContext _appDbContext;
        public AppDbContext Context { get => _appDbContext; }
        public AppDbTestContext ( )
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>( );
            builder.UseInMemoryDatabase(databaseName: "ApplicationDbInMemory");

            var dbContextOptions = builder.Options;
            _appDbContext = new AppDbContext(dbContextOptions);
            // Delete existing db before creating a new one
            _appDbContext.Database.EnsureDeleted( );
            _appDbContext.Database.EnsureCreated( );
        }
    }
}
