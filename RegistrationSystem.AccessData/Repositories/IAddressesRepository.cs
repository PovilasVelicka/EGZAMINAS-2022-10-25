using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.AccessData.Repositories
{
    public interface IAddressesRepository
    {
        Task<Address?> FindAddressAsync (string city, string street, string houseNumber, string appartmentNumber);
    }
}
