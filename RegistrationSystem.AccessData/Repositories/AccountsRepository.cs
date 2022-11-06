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
            await ChangePropertiesToExistsAsync(account);
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
            var account = await AccountsQuery( ).SingleOrDefaultAsync(a => a.LoginName == userLogin);
            return account;
        }

        public async Task UpdateAsync (Account account)
        {
            await ChangePropertiesToExistsAsync(account);
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

        private async Task ChangePropertiesToExistsAsync (Account account)
        {
            var phone = await _context.Phones.FirstOrDefaultAsync(p => p.Value == account.UserInfo.Phone.Value);
            var personalCode = await _context.PersonalCodes.FirstOrDefaultAsync(p => p.Value == account.UserInfo.PersonalCode.Value);
            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Value == account.UserInfo.Email.Value);
            var firstName = await _context.FirstNames.FirstOrDefaultAsync(f => f.Value == account.UserInfo.FirstName.Value);
            var lastName = await _context.LastNames.FirstOrDefaultAsync(f => f.Value == account.UserInfo.LastName.Value);
            var address = await _context.Addresses.FirstOrDefaultAsync(a =>
                                        a.City.Value == account.UserInfo.Address.City.Value
                                        && a.Street.Value == account.UserInfo.Address.Street.Value
                                        && a.HouseNumber.Value == account.UserInfo.Address.HouseNumber.Value
                                        && a.AppartmentNumber.Value == account.UserInfo.Address.AppartmentNumber.Value);

            account.UserInfo.Phone = phone ?? account.UserInfo.Phone;
            account.UserInfo.PersonalCode = personalCode ?? account.UserInfo.PersonalCode;
            account.UserInfo.Email = email ?? account.UserInfo.Email;
            account.UserInfo.FirstName = firstName ?? account.UserInfo.FirstName;
            account.UserInfo.LastName = lastName ?? account.UserInfo.LastName;
            if (address != null)
            {
                account.UserInfo.Address = address;
            }
            else
            {
                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Value == account.UserInfo.Address.City.Value);
                var street = await _context.Streets.FirstOrDefaultAsync(s => s.Value == account.UserInfo.Address.Street.Value);
                var houseNumber = await _context.HouseNumbers.FirstOrDefaultAsync(h => h.Value == account.UserInfo.Address.HouseNumber.Value);
                var appartmentNumber = await _context.AppartmentNumbers.FirstOrDefaultAsync(a => a.Value == account.UserInfo.Address.AppartmentNumber.Value);
                account.UserInfo.Address = new( )
                {
                    City = city ?? account.UserInfo.Address.City,
                    Street = street ?? account.UserInfo.Address.Street,
                    HouseNumber = houseNumber ?? account.UserInfo.Address.HouseNumber,
                    AppartmentNumber = appartmentNumber ?? account.UserInfo.Address.AppartmentNumber,
                };
            }
        }
    }
}
