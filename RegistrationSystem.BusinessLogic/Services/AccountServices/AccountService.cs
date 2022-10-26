using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    internal class AccountService : IAccountService
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IAddressesRepository _addressesRepository;
        public AccountService (IAccountsRepository accountRepository, IAddressesRepository addressesRepository)
        {
            _accountRepository = accountRepository;
            _addressesRepository = addressesRepository;
        }

        public async Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid accountId)
        {
            var account = await _accountRepository.GetAsync(accountId);
            return new ServiceResponseDto<Account>(account);
        }

        public async Task<IServiceResponseDto<string>> RemoveAccountAsync (Guid accountId, int userId)
        {
            var account = await _accountRepository.GetAsync(accountId);
            if (account.Role != UserRole.Admin) return new ServiceResponseDto<string>("You do not have permissions to remove user");

            // TODO: REMOV USER BY USER ID
            return new ServiceResponseDto<string>(true, "Account deleted successfuly");
        }

        public async Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid accountId, IUserInfoDto userInfo)
        {

            var account = await _accountRepository.GetAsync(accountId);

            if (account.UserInfo == null)
            {
                return new ServiceResponseDto<Account>("No user information found to update. First create user information with all fields");
            }

            await MapUserInfo(account, userInfo);

            return new ServiceResponseDto<Account>(account);
        }

        public async Task<IServiceResponseDto<Account>> CreateUserInfoAsync (Guid accountId, IUserInfoDto userInfo)
        {

            var account = await _accountRepository.GetAsync(accountId);

            if (account.UserInfo != null)
            {
                return new ServiceResponseDto<Account>("User information already exists!");
            }

            if (
               string.IsNullOrWhiteSpace(userInfo.FirstName)
            || string.IsNullOrWhiteSpace(userInfo.LastName)
            || string.IsNullOrWhiteSpace(userInfo.PersonalCode)
            || string.IsNullOrWhiteSpace(userInfo.Phone)
            || string.IsNullOrWhiteSpace(userInfo.Email)
            || userInfo.Photo == null
            || string.IsNullOrWhiteSpace(userInfo.City)
            || string.IsNullOrWhiteSpace(userInfo.Street)
            || string.IsNullOrWhiteSpace(userInfo.HouseNumber)
            || string.IsNullOrWhiteSpace(userInfo.AppartmentNumber)) new ServiceResponseDto<Account>("All fields are required to create user information!");

            await MapUserInfo(account, userInfo);

            await _accountRepository.UpdateAsync(account);
            return new ServiceResponseDto<Account>(account);
        }

        private async Task MapUserInfo (Account account, IUserInfoDto userInfo)
        {
            if (account.UserInfo == null) return;
            if (!string.IsNullOrWhiteSpace(userInfo.FirstName)) account.UserInfo.FirstName = userInfo.FirstName;
            if (!string.IsNullOrWhiteSpace(userInfo.LastName)) account.UserInfo.LastName = userInfo.LastName;
            if (!string.IsNullOrWhiteSpace(userInfo.PersonalCode)) account.UserInfo.PersonalCode = userInfo.PersonalCode;
            if (!string.IsNullOrWhiteSpace(userInfo.Phone)) account.UserInfo.Phone = userInfo.Phone;
            if (!string.IsNullOrWhiteSpace(userInfo.Email)) account.UserInfo.Email = userInfo.Email;
            if (userInfo.Photo != null) account.UserInfo!.Photo = userInfo.Photo;

            var city = string.IsNullOrWhiteSpace(userInfo.City) ? account.UserInfo.Address.City : userInfo.City;
            var street = string.IsNullOrWhiteSpace(userInfo.Street) ? account.UserInfo.Address.Street : userInfo.Street;
            var houseNumber = string.IsNullOrWhiteSpace(userInfo.HouseNumber) ? account.UserInfo.Address.HouseNumber : userInfo.HouseNumber;
            var appartmentNumber = string.IsNullOrWhiteSpace(userInfo.AppartmentNumber) ? account.UserInfo.Address.AppartmentNumber : userInfo.AppartmentNumber;


            var existsAddres = await _addressesRepository.FindAddressAsync(
                city,
                street,
                houseNumber,
                appartmentNumber);

            if (existsAddres != null)
            {
                account.UserInfo.Address = existsAddres;
            }
            else
            {
                account.UserInfo.Address = new Address( );
                account.UserInfo.Address.City = city;
                account.UserInfo.Address.Street = street;
                account.UserInfo.Address.HouseNumber = houseNumber;
                account.UserInfo.Address.AppartmentNumber = appartmentNumber;
            }
        }
    }
}
