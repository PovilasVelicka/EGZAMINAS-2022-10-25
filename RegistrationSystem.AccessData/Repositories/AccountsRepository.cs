using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("RegistrationSystemTests")]

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
            if (account.Id != Guid.Empty) throw new KeyNotFoundException("To add account id must by Guid.Empty");
            account.Id = Guid.NewGuid( );
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
                    + a.UserInfo.FirstName.Value
                    + a.UserInfo.LastName.Value
                    + a.UserInfo.Email.Value).Contains(nospaceSubstring))
                .ToListAsync( );
        }

        public async Task<Account> GetAsync (Guid userGuid)
        {
            return await AccountsQuery( ).SingleAsync(a => a.Id == userGuid);
        }

        public async Task<Account?> GetByLoginAsync (string userLogin)
        {
            return await AccountsQuery( ).FirstOrDefaultAsync(a => a.LoginName == userLogin);
        }

        public async Task UpdateAsync (Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync( );
        }

        private IQueryable<Account> AccountsQuery ( )
        {
            return _context.Accounts
                .Include(u => u.UserInfo.Phone)
                .Include(u => u.UserInfo.PersonalCode)
                .Include(u => u.UserInfo.Email)
                .Include(u => u.UserInfo.FirstName)
                .Include(u => u.UserInfo.LastName)
                .Include(a => a.UserInfo.Address.City)
                .Include(a => a.UserInfo.Address.Street)
                .Include(a => a.UserInfo.Address.HouseNumber)
                .Include(a => a.UserInfo.Address.AppartmentNumber)
                .AsQueryable<Account>( );
        }      
    }
}
