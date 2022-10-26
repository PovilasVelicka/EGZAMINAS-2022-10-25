using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    public interface IAccountService
    {
        Task<IServiceResponseDto<string>> SignupNewAccountAsync (string loginName, string password, IUserInfoDto userInfo);
        Task<IServiceResponseDto<string>> LoginAsync (string loginName, string password);
        Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid accountId);
        Task<IServiceResponseDto<string>> DeleteAccountAsync (Guid accountId, Guid userId);
        Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid accountId, IUserInfoDto userInfo);
        //Task<IServiceResponseDto<Account>> CreateUserInfoAsync (Guid accountId, IUserInfoDto userInfo);
    }
}
