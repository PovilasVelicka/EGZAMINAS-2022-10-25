using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.AccessData.Repositories
{
    internal class AccountsRepository : IAccountsRepository
    {
        private readonly AppDbContext _context;

        public AccountsRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync (Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync( );
            return account.Id;
        }

        public async Task<int> CountRoleAsync (UserRole role)
        {
            return await _context.Accounts.CountAsync(a => a.Role == role);
        }

        public async Task DeleteAsync (Guid id)
        {
            var account = await GetAsync(id);           
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync( );          
        }

        public async Task<List<Account>> GetAllAsync ( )
        {
            return await AccountsQuery( ).ToListAsync( );
        }

        public async Task<List<Account>> GetAllAsync (string searchSubstring)
        {
            var nospaceSubstring = searchSubstring.Replace(" ", "");
            return await AccountsQuery( )
                .Where(a => (
                    a.LoginName
                    + a.UserInfo.FirstName
                    + a.UserInfo.LastName
                    + a.UserInfo.Email).Contains(nospaceSubstring))
                .ToListAsync( );
        }

        public async Task<Account> GetAsync (Guid userGuid)
        {
            return await AccountsQuery( ).SingleAsync(a => a.Id.Equals(userGuid));
        }

        public async Task<Account?> GetByLoginAsync (string userLogin)
        {
            var account = await AccountsQuery( ).SingleOrDefaultAsync(a => a.LoginName == userLogin);
            return account;
        }

        public async Task UpdateAsync (Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync( );
        }

        private IQueryable<Account> AccountsQuery ( )
        {
            return _context.Accounts.Include(u => u.UserInfo).ThenInclude(a => a.Address);
        }
    }
}
