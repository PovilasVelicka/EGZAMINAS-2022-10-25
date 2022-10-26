using Microsoft.Extensions.Logging;
using NoteBook.Common.Interfaces.DataAccess;
using NoteBook.Common.Interfaces.DTOs;
using NoteBook.Common.Interfaces.Services;
using NoteBook.Entity.Enums;
using NoteBook.Entity.Models;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Net;
using Utilites.Exstensions;

namespace NoteBook.BusinessLogic.Services.AuthServices
{
    public class AuthService : IAuthService
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

        public async Task<string> LoginAsync (string username, string password)
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

            if (account.Disabled)
            {
                return new ServiceResponseDto<string>(null, "User disabled, please contact administrator", (int)HttpStatusCode.Forbidden);
            }

            return new ServiceResponseDto<string>(_jwtService.GetJwtToken(account), "Login succesfull", (int)HttpStatusCode.OK);
        }

        public async Task<ServiceResponseDto<string>> SignupNewAccountAsync (string loginName, string password, string email, string firstName, string lastName)
        {
            if (await _accountsRepository.GetByNameAsync(loginName) != null)
            {
                return new ServiceResponseDto<string>(null, "User name already exists", (int)HttpStatusCode.Conflict);
            }

            if (await _accountsRepository.GetByEmailAsync(email) != null)
            {
                return new ServiceResponseDto<string>(null, "Email already exists", (int)HttpStatusCode.Conflict);
            }

            var adminCount = await _accountsRepository.CountRoleAsync(Role.PeopleAdmin);

            var account = CreateAccount(loginName, password, email, adminCount == 0 ? Role.PeopleAdmin : Role.StandartUser, firstName, lastName);

            await _accountsRepository.AddAsync(account);

            try
            {
                await _accountsRepository.SaveChangesAsync( );
            }
            catch (Exception e)
            {
                string errMessage = $"Can't create user with: " +
                    $"\n\tlogin-name: {loginName}" +
                    $"\n\temail: {email}" +
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

        private static Account CreateAccount (string loginName, string password, string email, UserRole role)
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
