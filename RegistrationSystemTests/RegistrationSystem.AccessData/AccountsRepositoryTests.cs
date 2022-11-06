using Common.DTOs;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using System.Runtime.Versioning;


namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    [SupportedOSPlatform("windows")]
    public class AccountsRepositoryTests
    {
        private readonly AppDbTestContext _context;
        private readonly IAccountsRepository _sut;
        public AccountsRepositoryTests()
        {
            _context = new AppDbTestContext();
            _sut = new AccountsRepository(_context.Context);
        }

        [Fact]
        public async Task AddAsync_WhenAddNewAccount_ReturnGuid()
        {
            var account = new TestAccount(UserRole.User, generateGuid: false);

            var result = await _sut.AddAsync(account);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task AddAsync_WhenAddNewAccountWithGuid_ThrowKeyNotFoundException()
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            var errorHandled = false;

            try
            {
                var result = await _sut.AddAsync(account);
            }
            catch (KeyNotFoundException)
            {

                errorHandled = true;
            }

            Assert.True(errorHandled);
        }

        [Fact]
        public async Task CountRoleAsync_WhenHasAdminRole_ReturAdminCount()
        {
            for (var i = 0; i < 10; i++)
            {
                _context.Context.Accounts.Add(new TestAccount(UserRole.Admin, generateGuid: true));

            }
            _context.Context.SaveChanges();


            var actual = await _sut.CountRoleAsync(UserRole.Admin);

            Assert.Equal(10, actual);
        }

        [Fact]
        public async Task CountRoleAsync_WhenNoAdminRole_ReturZerro()
        {
            for (var i = 0; i < 10; i++)
            {
                _context.Context.Accounts.Add(new TestAccount(UserRole.User, generateGuid: true));

            }
            _context.Context.SaveChanges();

            var actual = await _sut.CountRoleAsync(UserRole.Admin);

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteAsync_CreateAndDelete()
        {
            var account = new TestAccount(UserRole.User, generateGuid: false);

            _context.Context.Accounts.Add(account);
            _context.Context.SaveChanges();
            var id = account.Id;

            var beforeDelete = _context.Context.Accounts.FirstOrDefault(u => u.Id == id);
            Assert.NotNull(beforeDelete);

            await _sut.DeleteAsync(id);
            var afterDelete = _context.Context.Accounts.FirstOrDefault(u => u.Id == id);
            Assert.Null(afterDelete);
        }

        [Fact]
        public async Task GetAllAsync_WhenSearchStringExists_ReturAccounts()
        {
            var admin = new TestAccount(UserRole.User, generateGuid: false);
            admin.UserInfo.FirstName.Value = "povilas";
            admin.UserInfo.LastName.Value = "velicka";
            admin.UserInfo.Email.Value = "emailas@cramo.com";

            _context.Context.Accounts.Add(admin);
            _context.Context.SaveChanges();

            var accounts = await _sut.GetAllAsync("las vel");
            var accountsCount = accounts.Count;
            Assert.Equal(1, accountsCount);


            accounts = await _sut.GetAllAsync("cramo");
            accountsCount = accounts.Count;
            Assert.Equal(1, accountsCount);

            accounts = await _sut.GetAllAsync("test");
            accountsCount = accounts.Count;
            Assert.Equal(0, accountsCount);
        }

        [Fact]
        public async Task GetAsync_WhenGuidExists_ReturnAccount()
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _context.Context.Accounts.Add(account);
            _context.Context.SaveChanges();

            var actual = await _sut.GetAsync(account.Id);

            Assert.Equal(account, actual);

        }

        [Fact]
        public async Task GetByLoginAsync_WhenLoginExists_ReturnAccount()
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _context.Context.Accounts.Add(account);
            _context.Context.SaveChanges();

            var actual = await _sut.GetByLoginAsync(account.LoginName);

            Assert.Equal(account.Id, actual!.Id);
        }

        [Fact]
        public async Task GetByLoginAsync_WhenLoginNotExists_ReturnNull()
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _context.Context.Accounts.Add(account);
            _context.Context.SaveChanges();

            var actual = await _sut.GetByLoginAsync(account.LoginName + "not-exists");

            Assert.Null(actual);
        }
    }
}
