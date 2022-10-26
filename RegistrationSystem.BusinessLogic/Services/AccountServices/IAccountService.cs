using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    public interface IAccountService
    {
        Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid accountId);
        Task<IServiceResponseDto<string>> RemoveAccountAsync (Guid accountId, int userId);
        Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid accountId, IUserInfoDto userInfo);
        Task<IServiceResponseDto<Account>> CreateUserInfoAsync (Guid accountId, IUserInfoDto userInfo);
    }
}
