using RegistrationSystem.Entities.Models.AccountProperties;

namespace RegistrationSystem.Common.Interfaces.AccessData
{
    public interface IUserInfoRepository
    {
        Task<Phone?> GetPhoneAsync (string phoneNumber);
        Task<PersonalCode?> GetPersonalCodeAsync (string personalCode);
        Task<Email?> GetEmailAsync (string email);
        Task<FirstName?> GetFirstNameAsync (string firstName);
        Task<LastName?> GetLastNameAsync (string lastName);
    }
}
