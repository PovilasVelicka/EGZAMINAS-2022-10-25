using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Models;

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
                    a.City.Name == city
                    && a.Street.Name == street
                    && a.HouseNumber == houseNumber
                    && a.AppartmentNumber == appartmentNumber);
            return address;
        }

        public async Task<City?> GetCityAsync (string cityName)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == cityName);
        }

        public async Task<Street?> GetStreetAsync (string streetName)
        {
            return await _context.Streets.FirstOrDefaultAsync(s => s.Name == streetName);
        }

        private IQueryable<Address> GetAddresses ( )
        {
            return _context
                .Addresses
                .Include(c => c.City)
                .Include(s => s.Street);
        }
    }
}
