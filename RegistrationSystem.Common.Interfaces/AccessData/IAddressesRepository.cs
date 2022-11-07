using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;

namespace RegistrationSystem.Common.Interfaces.AccessData
{
    public interface IAddressesRepository
    {
        Task<Address?> GetAddressAsync (string city, string street, string houseNumber, string appartmentNumber);
        Task<City?> GetCityAsync (string cityName);
        Task<Street?> GetStreetAsync (string streetName);       
        Task<HouseNumber?> GetHouseNumberAsync (string houseNumber);
        Task<AppartmentNumber?> GetAppartmentNumberAsync (string appartmentNumber);  

    }
}
