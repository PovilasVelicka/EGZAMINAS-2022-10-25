using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Data;
using System.Reflection;

namespace RegistrationSystem.AccessData
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;


        public AppDbContext ( ) { }

        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever( );

                entity.Property(p => p.Role)
                    .HasConversion(
                u => u.ToString( ),
                d => (UserRole)Enum.Parse(typeof(UserRole), d));
            });           
        }
    }
}
