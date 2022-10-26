using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.BusinessLogic.Services.AccountServices
{
    internal class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public Task<IServiceResponseDto<Account>> GetUserInfoAsync (Guid accountId)
        {
            throw new NotImplementedException( );
        }

        public Task<IServiceResponseDto<string>> RemoveAccountAsync (int userId)
        {
            throw new NotImplementedException( );
        }

        public Task<IServiceResponseDto<Account>> UpdateUserInfoAsync (Guid accountId, UserInfo account)
        {
            throw new NotImplementedException( );
        }
    }
}
