using RegistrationSystem.Entities.Models.AccountProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Common.Interfaces.AccessData
{
    public interface IPropertiesRepository
    {
        Task<Phone?> GetPhoneAsync(string phoneNumber);
        Task<PersonalCode?> GetPersonalCodeAsync(string personalCode);
        Task<Email?> GetEmailAsync(string email);
        Task<FirstName?> GetFirstNameAsync(string firstName);
        Task<LastName?> GetLastNameAsync(string lastName);
    }
}
