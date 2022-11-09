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

        public async Task<Address?> GetAddressAsync (string city, string street, string houseNumber, string appartmentNumber)
        {
            var address = await AddressQuery( )
                .FirstOrDefaultAsync(n =>
                    n.City.Value == city
                    && n.Street.Value == street
                    && n.HouseNumber.Value == houseNumber
                    && n.AppartmentNumber.Value == appartmentNumber);
            return address;
        }

        public async Task<AppartmentNumber?> GetAppartmentNumberAsync (string appartmentNumber)
        {
            return await _context.AppartmentNumbers.FirstOrDefaultAsync(n => n.Value == appartmentNumber);
        }

        public async Task<City?> GetCityAsync (string cityName)
        {
            return await _context.Cities.FirstOrDefaultAsync(n => n.Value == cityName);
        }

        public async Task<HouseNumber?> GetHouseNumberAsync (string houseNumber)
        {
            return await _context.HouseNumbers.FirstOrDefaultAsync(n => n.Value == houseNumber);
        }

        public async Task<Street?> GetStreetAsync (string streetName)
        {
            return await _context.Streets.FirstOrDefaultAsync(n => n.Value == streetName);         
        }

        private IQueryable<Address> AddressQuery ( )
        {
            return _context
                .Addresses
                .Include(c => c.City)
                .Include(s => s.Street)
                .Include(h => h.HouseNumber)
                .Include(a => a.AppartmentNumber);
        }
    }
}
