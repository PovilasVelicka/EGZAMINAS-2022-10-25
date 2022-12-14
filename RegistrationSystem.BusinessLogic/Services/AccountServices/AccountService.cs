using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.BusinessLogic.Services.AuthServices;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.Versioning;
using Utilites.Exstensions;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    [SupportedOSPlatform("windows")]
    internal class AccountService : IAccountService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IAddressesRepository _addressesRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserInfoRepository _propertiesRepository;

        public AccountService (
            IAccountsRepository accountRepository,
            IAddressesRepository addressesRepository,
            IUserInfoRepository propertiesRepository,
            IJwtService jwtService)
        {
            _addressesRepository = addressesRepository;
            _accountsRepository = accountRepository;
            _propertiesRepository = propertiesRepository;
            _jwtService = jwtService;
        }

        public async Task<IServiceResponseDto<string>> LoginAsync (string loginName, string password)
        {
            var account = await _accountsRepository.GetByLoginAsync(loginName);

            if (account == null)
            {
                return new ServiceResponseDto<string>(null, "User name not exists", (int)HttpStatusCode.NotFound);
            }

            if (!password.VerifyPassword(account.PasswordHash, account.PasswordSalt))
            {
                return new ServiceResponseDto<string>(null, "Incorrect password", (int)HttpStatusCode.Unauthorized);
            }

            return new ServiceResponseDto<string>(_jwtService.GetJwtToken(account), "Login succesfull", (int)HttpStatusCode.OK);
        }

        public async Task<IServiceResponseDto<string>> SignupAccountAsync (string loginName, string password, IUserInfoDto userInfo)
        {
            if (await _accountsRepository.GetByLoginAsync(loginName) != null)
            {
                return new ServiceResponseDto<string>(
                    null,
                    "Username already exists",
                    (int)HttpStatusCode.Conflict);
            }

            if (!userInfo.IsAllPropertiesNotEmpty( ))
            {
                return new ServiceResponseDto<string>(
                    null,
                    "All fields are required to create a user",
                    (int)HttpStatusCode.BadRequest);
            }

            var adminCount = await _accountsRepository.CountRoleAsync(UserRole.Admin);

            var account = CreateAccount(loginName, password, adminCount == 0 ? UserRole.Admin : UserRole.User);

            await MapAccountFromUserInfoDtoAsync(account, userInfo);

            await _accountsRepository.AddAsync(account);

            return new ServiceResponseDto<string>(
                _jwtService.GetJwtToken(account),
                "Account created successfully",
                (int)HttpStatusCode.Created);
        }

        public async Task<IServiceResponseDto<string>> DeleteAccountAsync (Guid adminGuid, Guid userGuid)
        {
            if (!await IsUserAdmin(adminGuid)) return new ServiceResponseDto<string>("You do not have permission to delete a user");

            if (adminGuid == userGuid) return new ServiceResponseDto<string>("You do not have rights to delete your own account");

            await _accountsRepository.DeleteAsync(userGuid);

            return new ServiceResponseDto<string>("Account successfully deleted", true);
        }

        public async Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid userGuid)
        {
            var account = await _accountsRepository.GetAsync(userGuid);

            return new ServiceResponseDto<Account>(account);
        }

        public async Task<IServiceResponseDto<List<Account>>> GetUsersAsync (Guid adminGuid, string searchSubstring)
        {
            if (!await IsUserAdmin(adminGuid)) return new ServiceResponseDto<List<Account>>("You do not have permission to view users");

            var accounts = await _accountsRepository.GetAllAsync(searchSubstring);

            return new ServiceResponseDto<List<Account>>(accounts);
        }

        public async Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid userGuid, IUserInfoDto userInfo)
        {
            var account = await _accountsRepository.GetAsync(userGuid);

            if (account == null) return new ServiceResponseDto<Account>("User is not found");

            await MapAccountFromUserInfoDtoAsync(account, userInfo);

            await _accountsRepository.UpdateAsync(account);

            return new ServiceResponseDto<Account>(account);
        }

        private async Task MapAccountFromUserInfoDtoAsync (Account account, IUserInfoDto userInfoDto)
        {
            await MapAccountUserInfoAsync(account, userInfoDto);
            await MapAccountAddressAsync(account, userInfoDto);
        }

        private async Task MapAccountUserInfoAsync (Account account, IUserInfoDto userInfo)
        {
            if (userInfo.Phone != null)
            {
                account.UserInfo.Phone =
                    await _propertiesRepository.GetPhoneAsync(userInfo.Phone) ??
                    new Phone { Value = userInfo.Phone };
            };

            if (userInfo.PersonalCode != null)
            {
                account.UserInfo.PersonalCode =
                    await _propertiesRepository.GetPersonalCodeAsync(userInfo.PersonalCode) ??
                    new PersonalCode { Value = userInfo.PersonalCode };
            }

            if (userInfo.Email != null)
            {
                account.UserInfo.Email =
                    await _propertiesRepository.GetEmailAsync(userInfo.Email) ??
                    new Email { Value = userInfo.Email };
            }

            if (userInfo.LastName != null)
            {
                account.UserInfo.LastName =
                    await _propertiesRepository.GetLastNameAsync(userInfo.LastName) ??
                    new LastName { Value = userInfo.LastName };
            }

            if (userInfo.FirstName != null)
            {
                account.UserInfo.FirstName =
                    await _propertiesRepository.GetFirstNameAsync(userInfo.FirstName) ??
                    new FirstName { Value = userInfo.FirstName };
            }

            if (userInfo.ProfilePicture != null) account.UserInfo.ProfilePicture =
                    ResizeImage(userInfo.ProfilePicture, userInfo.ContentType!, 200, 200);
        }

        private async Task MapAccountAddressAsync (Account account, IUserInfoDto userInfo)
        {
            if (userInfo.City != null || userInfo.Street != null || userInfo.HouseNumber != null || userInfo.AppartmentNumber != null)
            {
                City city;
                Street street;
                HouseNumber houseNumber;
                AppartmentNumber appartmentNumber;

                if (userInfo.City != null)
                    city = await _addressesRepository.GetCityAsync(userInfo.City) ?? new City { Value = userInfo.City };
                else
                    city = account.UserInfo.Address.City;


                if (userInfo.Street != null)
                    street = await _addressesRepository.GetStreetAsync(userInfo.Street) ?? new Street { Value = userInfo.Street };
                else
                    street = account.UserInfo.Address.Street;


                if (userInfo.HouseNumber != null)
                    houseNumber = await _addressesRepository.GetHouseNumberAsync(userInfo.HouseNumber) ?? new HouseNumber { Value = userInfo.HouseNumber };
                else
                    houseNumber = account.UserInfo.Address.HouseNumber;


                if (userInfo.AppartmentNumber != null)
                    appartmentNumber = await _addressesRepository.GetAppartmentNumberAsync(userInfo.AppartmentNumber) ?? new AppartmentNumber { Value = userInfo.AppartmentNumber };
                else
                    appartmentNumber = account.UserInfo.Address.AppartmentNumber;


                account.UserInfo.Address =
                    await _addressesRepository.GetAddressAsync(
                        city: city.Value,
                        street: street.Value,
                        houseNumber: houseNumber.Value,
                        appartmentNumber: appartmentNumber.Value)
                    ?? new Address
                    {
                        City = city,
                        Street = street,
                        HouseNumber = houseNumber,
                        AppartmentNumber = appartmentNumber
                    };
            }
        }

        private static Account CreateAccount (string loginName, string password, UserRole role)
        {
            var (passwordHash, passwordSalt) = password.CreatePasswordHash( );

            return new Account
            {
                LoginName = loginName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = role,
                UserInfo = new( )
                {
                    Address = new( )
                }
            };
        }

        private async Task<bool> IsUserAdmin (Guid adminGuid)
        {
            var account = await _accountsRepository.GetAsync(adminGuid);
            if (account == null || account.Role == UserRole.Admin) return true;
            return false;
        }

        private static byte[ ] ResizeImage (byte[ ] imageBytes, string contentType, int width, int height)
        {
            using MemoryStream ms = new( );
            using var img = Image.FromStream(new MemoryStream(imageBytes));
            var thumbnail = img.GetThumbnailImage(width, height, null, new IntPtr( ));
            thumbnail.Save(ms, GetImageFormat(contentType));
            return ms.ToArray( );
        }

        private static ImageFormat GetImageFormat (string contentType)
        {
            return contentType switch
            {
                "image/jpeg" => ImageFormat.Jpeg,
                "image/png" => ImageFormat.Png,
                "image/gif" => ImageFormat.Gif,
                _ => ImageFormat.Jpeg,
            };
        }
    }
}
