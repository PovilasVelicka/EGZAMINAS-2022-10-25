using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    public interface IAccountService
    {
        Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid accountId);
        Task<IServiceResponseDto<string>> RemoveAccountAsync (int userId);
        Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid accountId, UserInfo account);

    }
}
