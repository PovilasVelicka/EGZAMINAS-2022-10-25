using AutoFixture.Xunit2;
using Common.DTOs;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    [SupportedOSPlatform("windows")]    
    public class AccountsRepositoryTests
    {
        private readonly AppDbTestContext _Db;
        private readonly IAccountsRepository _sut;
        public AccountsRepositoryTests ( )
        {
            _Db = new AppDbTestContext("AccountsRepositoryTestsDb");
            _sut = new AccountsRepository(_Db.Context);
        }

        [Fact]
        public async Task AddAsync_WhenAddNewAccount_ReturnGuid ( )
        {
            var account = new TestAccount(UserRole.User, generateGuid: false);

            var result = await _sut.AddAsync(account);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task AddAsync_WhenAddNewAccountWithGuid_ThrowKeyNotFoundException ( )
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
        public async Task CountRoleAsync_WhenHasAdminRole_ReturAdminCount ( )
        {
            for (var i = 0; i < 10; i++)
            {
                _Db.Context.Accounts.Add(new TestAccount(UserRole.Admin, generateGuid: true));

            }
            _Db.Context.SaveChanges( );


            var actual = await _sut.CountRoleAsync(UserRole.Admin);

            Assert.Equal(10, actual);
        }

        [Fact]
        public async Task CountRoleAsync_WhenNoAdminRole_ReturZerro ( )
        {
            for (var i = 0; i < 10; i++)
            {
                _Db.Context.Accounts.Add(new TestAccount(UserRole.User, generateGuid: true));

            }
            _Db.Context.SaveChanges( );

            var actual = await _sut.CountRoleAsync(UserRole.Admin);

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteAsync_CreateAndDelete ( )
        {
            var account = CreateDbAccount( );

            var id = account.Id;

            var beforeDelete = _Db.Context.Accounts.FirstOrDefault(u => u.Id == id);
            Assert.NotNull(beforeDelete);

            await _sut.DeleteAsync(id);
            var afterDelete = _Db.Context.Accounts.FirstOrDefault(u => u.Id == id);
            Assert.Null(afterDelete);
        }

        [Fact]
        public async Task GetAllAsync_WhenSearchStringExists_ReturAccounts ( )
        {
            var admin = new TestAccount(UserRole.User, generateGuid: true);
            admin.UserInfo.FirstName.Value = "povilas";
            admin.UserInfo.LastName.Value = "velicka";
            admin.UserInfo.Email.Value = "emailas@cramo.com";

            _Db.Context.Accounts.Add(admin);
            _Db.Context.SaveChanges( );

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
        public async Task GetAsync_WhenGuidExists_ReturnAccount ( )
        {
            var account = CreateDbAccount( );

            var actual = await _sut.GetAsync(account.Id);

            Assert.Equal(account.Id, actual.Id);
        }

        [Fact]
        public async Task GetByLoginAsync_WhenLoginExists_ReturnAccount ( )
        {
            var account = CreateDbAccount( );
       
            var actual = await _sut.GetByLoginAsync(account.LoginName);

            Assert.Equal(account.Id, actual!.Id);
        }

        [Fact]
        public async Task GetByLoginAsync_WhenLoginNotExists_ReturnNull ( )
        {
            var account = CreateDbAccount( );

            var actual = await _sut.GetByLoginAsync(account.LoginName + "not-exists");

            Assert.Null(actual);
        }

        [Theory, AutoData]
        public async Task UpdateAsync_WhenUpdateUserInfoFirstName_ValueChanged (string expected)
        {
            var account = CreateDbAccount( );

            account.UserInfo.FirstName.Value = expected;
            var returned = await GetAfterUpdate(account);
            var actual = returned.UserInfo.FirstName.Value;
            Assert.Equal(expected, actual);
        }


        [Theory, AutoData]
        public async Task UpdateAsync_WhenUpdateAddressCity_ValueChanged (string expected)
        {
            var account = CreateDbAccount( );

            account.UserInfo.Address.City.Value = expected;
            var returned = await GetAfterUpdate(account);
            var actual = returned.UserInfo.Address.City.Value;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoData]
        public async Task UpdateAsync_WhenUpdateUserRole_ValueChanged (UserRole expected)
        {
            var account = CreateDbAccount( );

            account.Role = expected;
            var returned = await GetAfterUpdate(account);
            var actual = returned.Role;
            Assert.Equal(expected, actual);
        }

        private Account CreateDbAccount ( )
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _Db.Context.Accounts.Add(account);
            _Db.Context.SaveChanges( );
            return account;
        }

        private async Task<Account> GetAfterUpdate (Account account)
        {
            _Db.Context.Accounts.Update(account);
            _Db.Context.SaveChanges( );
            return await _sut.GetAsync(account.Id);
        }

    }
}
