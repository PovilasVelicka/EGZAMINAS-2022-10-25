﻿using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.Common.Interfaces.AccessData
{
    public interface IAccountsRepository
    {
        Task<Guid> AddAsync (Account account);
        Task UpdateAsync (Account account);
        Task DeleteAsync (Guid id);
        Task<Account> GetAsync (Guid id);
        Task<List<Account>> GetAllAsync ();
    }
}
