using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("RegistrationSystemTests")]
namespace RegistrationSystem.AccessData
{

    internal class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;    
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<PersonalCode> PersonalCodes { get; set; } = null!;
        public DbSet<Email> Emails { get; set; }=null!;
        public DbSet<FirstName> FirstNames { get; set; }  =null!;
        public DbSet<LastName> LastNames { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Street> Streets { get; set; }=null!;
        public DbSet<HouseNumber> HouseNumbers { get; set; } = null!;
        public DbSet<AppartmentNumber> AppartmentNumbers { get; set; } = null!;
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

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasMany(u => u.UserInfos)
                .WithOne(u => u.Address)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
