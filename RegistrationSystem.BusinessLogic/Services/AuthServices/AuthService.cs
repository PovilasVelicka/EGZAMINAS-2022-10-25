using Microsoft.Extensions.Logging;
using NoteBook.Common.Interfaces.DataAccess;
using NoteBook.Common.Interfaces.DTOs;
using NoteBook.Common.Interfaces.Services;
using NoteBook.Entity.Enums;
using NoteBook.Entity.Models;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Net;
using Utilites.Exstensions;

namespace NoteBook.BusinessLogic.Services.AuthServices
{
    internal class AuthService : IAuthService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        public AuthService (IAccountsRepository accountsRepository, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _accountsRepository = accountsRepository;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<IServiceResponseDto<string>> LoginAsync (string username, string password)
        {
            var account = await _accountsRepository.GetByNameAsync(username);

            if (account == null)
            {
                return new ServiceResponseDto<string>(default, "User name not exists", (int)HttpStatusCode.NotFound);
            }

            if (!password.VerifyPassword(account.PasswordHash, account.PasswordSalt))
            {
                return new ServiceResponseDto<string>(null, "Incorrect password", (int)HttpStatusCode.Unauthorized);
            }   

            return new ServiceResponseDto<string>(_jwtService.GetJwtToken(account), "Login succesfull", (int)HttpStatusCode.OK);
        }

        public async Task<IServiceResponseDto<string>> SignupNewAccountAsync (string loginName, string password)
        {
            if (await _accountsRepository.GetByNameAsync(loginName) != null)
            {
                return new ServiceResponseDto<string>(null, "User name already exists", (int)HttpStatusCode.Conflict);
            }

            var adminCount = await _accountsRepository.CountRoleAsync(UserRole.Admin);

            var account = CreateAccount(loginName, password, adminCount == 0 ? UserRole.Admin : UserRole.User);

            try
            {
                await _accountsRepository.AddAsync(account);
            }
            catch (Exception e)
            {
                string errMessage = $"Can't create user with: " +
                    $"\n\tlogin-name: {loginName}" +      
                    $"\n\terror: {e.Message} {e.InnerException}";
                _logger.LogError(message: errMessage);

                return new ServiceResponseDto<string>(
                    null, 
                    e.InnerException?.Message ?? "Unexpected error", 
                    (int)HttpStatusCode.ServiceUnavailable);
            }

            _logger.Log(
                LogLevel.Information,
                $"New user created: " +
                $"\n\tId: {account.Id}" +
                $"\n\tName: {account.LoginName}");

            return new ServiceResponseDto<string>(_jwtService.GetJwtToken(account), "", (int)HttpStatusCode.Created);
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
            };
        } 
    }
}
