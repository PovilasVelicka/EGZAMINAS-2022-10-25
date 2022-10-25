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

        public async Task<Address?> FindAddressAsync (string city, string street, string houseNumber, string appartmentNumber)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a =>
                a.City == city
                && a.Street == street
                && a.HouseNumber == houseNumber
                && a.AppartmentNumber == appartmentNumber);
            return address;
        }
    }
}
