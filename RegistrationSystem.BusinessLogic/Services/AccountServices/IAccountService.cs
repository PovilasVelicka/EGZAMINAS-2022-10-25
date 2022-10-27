using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    public interface IAccountService
    {
        Task<IServiceResponseDto<string>> SignupAccountAsync (string loginName, string password, IUserInfoDto userInfo);

        Task<IServiceResponseDto<string>> LoginAsync (string loginName, string password);

        Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid userGuid);

        Task<IServiceResponseDto<List<Account>>> GetUsersAsync (Guid adminGuid, string searchSubstring);

        Task<IServiceResponseDto<string>> DeleteAccountAsync (Guid adminGuid, Guid userGuid);

        Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid userGuid, IUserInfoDto userInfo);
    }
}
