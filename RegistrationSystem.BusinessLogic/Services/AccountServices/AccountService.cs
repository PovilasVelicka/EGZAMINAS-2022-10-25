using Microsoft.Extensions.Logging;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.BusinessLogic.Services.AuthServices;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
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
        private readonly ILogger<AccountService> _logger;
        public AccountService (
            IAccountsRepository accountRepository,
            IAddressesRepository addressesRepository,
            IJwtService jwtService,
            ILogger<AccountService> logger)
        {
            _accountsRepository = accountRepository;
            _addressesRepository = addressesRepository;
            _jwtService = jwtService;
            _logger = logger;
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
                    "User name already exists",
                    (int)HttpStatusCode.Conflict);
            }

            if (!userInfo.IsAllPropertiesNotEmpty( ))
            {
                return new ServiceResponseDto<string>(
                    null,
                    "All fields are required to create user",
                    (int)HttpStatusCode.BadRequest);
            }

            var adminCount = await _accountsRepository.CountRoleAsync(UserRole.Admin);

            var account = CreateAccount(loginName, password, adminCount == 0 ? UserRole.Admin : UserRole.User);

            await MapUserInfo(account, userInfo);

            await _accountsRepository.AddAsync(account);

            return new ServiceResponseDto<string>(
                _jwtService.GetJwtToken(account),
                "Account created successfuly",
                (int)HttpStatusCode.Created);
        }

        public async Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid userGuid)
        {
            var account = await _accountsRepository.GetAsync(userGuid);

            return new ServiceResponseDto<Account>(account);
        }

        public async Task<IServiceResponseDto<List<Account>>> GetUsersAsync (Guid adminGuid, string searchSubstring)
        {
            if (!await IsUserAdmin(adminGuid)) return new ServiceResponseDto<List<Account>>("You do not have permissions to view users");

            var accounts = await _accountsRepository.GetAllAsync(searchSubstring);

            return new ServiceResponseDto<List<Account>>(accounts);
        }

        public async Task<IServiceResponseDto<string>> DeleteAccountAsync (Guid adminGuid, Guid userGuid)
        {
            if (!await IsUserAdmin(adminGuid)) return new ServiceResponseDto<string>("You do not have permissions to delete user");

            await _accountsRepository.DeleteAsync(userGuid);

            return new ServiceResponseDto<string>(true, "Account deleted successfuly");
        }

        public async Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid userGuid, IUserInfoDto userInfo)
        {
            var account = await _accountsRepository.GetAsync(userGuid);

            if (account == null) return new ServiceResponseDto<Account>("User not found");

            await MapUserInfo(account, userInfo);

            await _accountsRepository.UpdateAsync(account);

            return new ServiceResponseDto<Account>(account);
        }

        private async Task MapUserInfo (Account account, IUserInfoDto userInfo)
        {
            account.UserInfo.Phone = userInfo.Phone ?? account.UserInfo.Phone;
            account.UserInfo.Email = userInfo.Email ?? account.UserInfo.Email;
            account.UserInfo.LastName = userInfo.LastName ?? account.UserInfo.LastName;
            account.UserInfo.FirstName = userInfo.FirstName ?? account.UserInfo.FirstName;
            account.UserInfo.PersonalCode = userInfo.PersonalCode ?? account.UserInfo.PersonalCode;
            if (userInfo.ProfilePicture != null)
                account.UserInfo.ProfilePicture =
                    ResizeImage(userInfo.ProfilePicture, userInfo.ContentType!, 200, 200);

            var city = userInfo.City ?? account.UserInfo.Address.City;
            var street = userInfo.Street ?? account.UserInfo.Address.Street;
            var houseNumber = userInfo.HouseNumber ?? account.UserInfo.Address.HouseNumber;
            var appartmentNumber = userInfo.AppartmentNumber ?? account.UserInfo.Address.AppartmentNumber;


            var existsAddres = await _addressesRepository.FindAddressAsync(
                city,
                street,
                houseNumber,
                appartmentNumber);

            account.UserInfo
                .Address = existsAddres ?? new( )
                {
                    City = city,
                    Street = street,
                    HouseNumber = houseNumber,
                    AppartmentNumber = appartmentNumber
                };
        }

        private static Account CreateAccount (string loginName, string password, UserRole role)
        {
            var (passwordHash, passwordSalt) = password.CreatePasswordHash( );

            return new Account
            {
                Id = Guid.NewGuid( ),
                LoginName = loginName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = role,
                UserInfo = new( )
                {
                    Address = new( )
                },
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
                "image/JPEG" => ImageFormat.Jpeg,
                "image/PNG" => ImageFormat.Png,
                "image/GIF" => ImageFormat.Gif,
                _ => ImageFormat.Jpeg,
            };
        }
    }
}
