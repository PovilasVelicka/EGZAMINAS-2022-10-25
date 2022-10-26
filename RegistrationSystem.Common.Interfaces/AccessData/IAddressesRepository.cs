using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.Common.Interfaces
{
    public interface IAddressesRepository
    {
        Task<Address?> FindAddressAsync (string city, string street, string houseNumber, string appartmentNumber);
    }
}
