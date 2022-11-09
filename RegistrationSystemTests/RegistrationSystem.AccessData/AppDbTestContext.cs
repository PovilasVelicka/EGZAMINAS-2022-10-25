using Microsoft.EntityFrameworkCore;
using RegistrationSystem.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Reflection.Emit;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    internal class AppDbTestContext : DbContext
    {
        private readonly AppDbContext _appDbContext;
        public AppDbContext Context { get => _appDbContext; }
        public AppDbTestContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(databaseName: dbName);

            var dbContextOptions = builder.Options;
            _appDbContext = new AppDbContext(dbContextOptions);
      
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Database.EnsureCreated();

        }
    }
}
