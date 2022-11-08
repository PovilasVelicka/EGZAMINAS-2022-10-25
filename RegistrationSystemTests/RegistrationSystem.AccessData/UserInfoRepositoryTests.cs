using AutoFixture.Xunit2;
using Common.DTOs;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    [SupportedOSPlatform("windows")]
    public class UserInfoRepositoryTests
    {
        private readonly AppDbTestContext _Db;
        private readonly IUserInfoRepository _sut;

        public UserInfoRepositoryTests ( )
        {
            _Db = new AppDbTestContext("UserInfoRepositoryTestsDb");
            _sut = new UserInfoRepository(_Db.Context);
        }

        [Fact]
        public async Task GetEmailAsync_WhenEmailExists_ReturnEmail ( )
        {
            var expected = CreateDbAccount().UserInfo.Email.Value ;

            var actual =  await _sut.GetEmailAsync(expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Value);
        }
        [Theory,AutoData]
        public async Task GetEmailAsync_WhenEmailNotExists_ReturnNull (string expected )
        {
            var result = await _sut.GetEmailAsync ( expected );

            Assert.Null(result);
        }
        [Fact]
        public async Task GetFirstNameAsync_WhenFristNameExists_ReturnFirstName ( )
        {
            var expected = CreateDbAccount( ).UserInfo.FirstName.Value;

            var actual = await _sut.GetFirstNameAsync(expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Value);
        }

        [Theory, AutoData]
        public async Task GetFirstNameAsync_WhenFirstNameNotExists_ReturnNull (string expected)
        {
            var result = await _sut.GetFirstNameAsync(expected);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetLastNameAsync_WhenLastNameExists_ReturnLastName ( )
        {
            var expected = CreateDbAccount( ).UserInfo.LastName.Value;

            var actual = await _sut.GetLastNameAsync(expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Value);
        }

        [Theory, AutoData]
        public async Task GetLastNameAsync_WhenLastNameNotExists_ReturnNull (string expected)
        {
            var result = await _sut.GetLastNameAsync(expected);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPersonalCodeAsync_WhenPersonalCodeExists_ReturnPersonalCode ()
        {
            var expected = CreateDbAccount( ).UserInfo.PersonalCode.Value;

            var actual = await _sut.GetPersonalCodeAsync(expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Value);
        }
        [Theory, AutoData]
        public async Task GetPersonalCodeAsync_WhenPersonalCodeNotExists_ReturnNull (string expected)
        {
            var result = await _sut.GetPersonalCodeAsync(expected);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPhoneAsync_WhenPhoneExists_ReturnPhone ( )
        {
            var expected = CreateDbAccount( ).UserInfo.Phone.Value;

            var actual = await _sut.GetPhoneAsync(expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Value);
        }
        [Theory, AutoData]
        public async Task GetPhoneAsync_WhenPhoneNotExists_ReturnNull (string expected)
        {
            var result = await _sut.GetPhoneAsync(expected);

            Assert.Null(result);
        }

        private Account CreateDbAccount ( )
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _Db.Context.Accounts.Add(account);
            _Db.Context.SaveChanges( );
            return account;
        }
    }
}
