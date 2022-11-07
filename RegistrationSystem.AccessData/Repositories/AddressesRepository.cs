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
            var number = await _context.AppartmentNumbers.FirstOrDefaultAsync(n => n.Value == appartmentNumber);
            number ??= new AppartmentNumber { Value = appartmentNumber };
            return number;
        }

        public async Task<City?> GetCityAsync (string cityName)
        {
            var name = await _context.Cities.FirstOrDefaultAsync(n => n.Value == cityName);
            name ??= new City { Value = cityName };
            return name;
        }

        public async Task<HouseNumber?> GetHouseNumberAsync (string houseNumber)
        {
            var number = await _context.HouseNumbers.FirstOrDefaultAsync(n => n.Value == houseNumber);
            number ??= new HouseNumber { Value = houseNumber };
            return number;
        }

        public async Task<Street?> GetStreetAsync (string streetName)
        {
            var name = await _context.Streets.FirstOrDefaultAsync(n => n.Value == streetName);
            name??= new Street { Value = streetName };
            return name;
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
