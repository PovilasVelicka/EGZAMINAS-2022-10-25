using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.Common.Interfaces.AccessData
{
    public interface IAccountsRepository
    {
        Task<Guid> AddAsync (Account account);

        Task UpdateAsync (Account account);

        Task<bool> DeleteAsync (Guid id);

        Task<Account> GetAsync (Guid id);

        Task<Account> GetByUserIdAsync (int id);

        Task<List<Account>> GetAllAsync ( );

        Task<Account?> GetByLoginAsync (string userLogin);

        Task<int> CountRoleAsync (UserRole role);
    }
}
