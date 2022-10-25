using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Common.Interfaces.AccessData;
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

        public async Task DeleteAsync (Guid id)
        {
            var account = await GetAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync( );
        }

        public async Task<List<Account>> GetAllAsync ( )
        {
            return await _context.Accounts.ToListAsync( );
        }

        public async Task<Account> GetAsync (Guid id)
        {
            return await _context.Accounts.SingleAsync(a => a.Id.Equals(id));
        }

        public async Task UpdateAsync (Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync( );
        }
    }
}
