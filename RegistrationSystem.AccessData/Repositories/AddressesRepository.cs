using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;

namespace RegistrationSystem.AccessData.Repositories
{
    internal class AddressesRepository : IAddressesRepository
    {
        private readonly AppDbContext _context;

        public AddressesRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address?> FindAddressAsync (
            string city, string street,
            string houseNumber, string appartmentNumber)
        {
            var address = await GetAddresses( )
                .FirstOrDefaultAsync(a =>
                    a.City.Value == city
                    && a.Street.Value == street
                    && a.HouseNumber.Value == houseNumber
                    && a.AppartmentNumber.Value == appartmentNumber);
            return address;
        }

        public async Task<City?> GetCityAsync (string cityName)
        {
            return (await _context.Addresses.FirstOrDefaultAsync(c => c.City.Value == cityName))?.City;
        }

        public async Task<Street?> GetStreetAsync (string streetName)
        {
            return (await _context.Addresses.FirstOrDefaultAsync(c => c.Street.Value == streetName))?.Street;
        }

        private IQueryable<Address> GetAddresses ( )
        {
            return _context
                .Addresses
                .Include(c => c.City)
                .Include(s => s.Street)
                .Include(h=> h.HouseNumber)
                .Include(a=> a.AppartmentNumber);
        }
    }
}
