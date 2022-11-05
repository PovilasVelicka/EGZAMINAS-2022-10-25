using AutoFixture;
using AutoFixture.Xunit2;
using Common.Attributes;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Moq;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.BusinessLogic.Services.AuthServices;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;
using Utilites.Exstensions;

namespace RegistrationSystemTests.RegistrationSystem.BusinessLogic
{
    [SupportedOSPlatform("windows")]
    public class AccountServiceTests
    {
        private readonly Mock<IAccountsRepository> _accountsRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;

        private readonly Mock<IAddressesRepository> _addressesRepositoryMock;
        private readonly IAccountService _sut;
        private readonly IFixture _fixture;
        private readonly Mock<IFormFile> _formFileMock;
        public AccountServiceTests ( )
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>( );
            _jwtServiceMock = new Mock<IJwtService>( );
            _addressesRepositoryMock = new Mock<IAddressesRepository>( );
            _sut = new AccountService(
                _accountsRepositoryMock.Object,
                _addressesRepositoryMock.Object,
                _jwtServiceMock.Object);
            _fixture = new Fixture( );
            _formFileMock = new Mock<IFormFile>( );
        }

        [Theory, AutoData]
        public async Task LoginAsync_WhenUserNameNotExists_ReturnNotFound (string loginName, string password)
        {
            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(( ) => null);

            var result = await _sut.LoginAsync(loginName, password);

            Assert.Equal(404, result.StatusCode);
        }

        [Theory, AutoData]
        public async Task LoginAsync_WhenUserNametExistsPassowrIsIncorrect_ReturnUnauthorized (string loginName, string password)
        {

            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(
                new Account
                {
                    LoginName = loginName,
                    PasswordHash = _fixture.Create<byte[ ]>( ),
                    PasswordSalt = _fixture.Create<byte[ ]>( )
                });

            var result = await _sut.LoginAsync(loginName, password);

            Assert.Equal(401, result.StatusCode);
        }

        [Theory, AutoData]
        public async Task LoginAsync_WhenUserNametExistsPassowrCorrect_ReturnOk (string loginName, string password)
        {
            var passwordSicret = password.CreatePasswordHash( );

            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(
                new Account
                {
                    LoginName = loginName,
                    PasswordHash = passwordSicret.hash,
                    PasswordSalt = passwordSicret.salt
                });

            var result = await _sut.LoginAsync(loginName, password);

            Assert.Equal(200, result.StatusCode);
        }

        [Theory, AutoData]
        public async Task SignupAccountAsync_WhenLoginNameAlreadyExists_ReturnConflict (string loginName, string password, UserInfoDto userInfoDto)
        {
            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(new Account
                {
                    LoginName = loginName,
                    PasswordHash = _fixture.Create<byte[ ]>( ),
                    PasswordSalt = _fixture.Create<byte[ ]>( )
                });

            var response = await _sut.SignupAccountAsync(loginName, password, userInfoDto);

            Assert.Equal(409, response.StatusCode);
        }

        [Theory, AutoData]
        public async Task SignupAccountAsync_WhenNotAllPropertiesSet_ReturnBadRequest (string loginName, string password, UserInfoDto userInfoDto)
        {
            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(( ) => null);

            userInfoDto.Email = null;

            var response = await _sut.SignupAccountAsync(loginName, password, userInfoDto);

            Assert.Equal(400, response.StatusCode);
        }

        [Theory, AutoData]
        public async Task SignupAccountAsync_WhenUserCreated_ReturnCreated (string loginName, string password, UserInfoDto userInfoDto)
        {
            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(( ) => null);

            _accountsRepositoryMock
                .Setup(a => a.CountRoleAsync(UserRole.Admin))
                .ReturnsAsync(0);

            _formFileMock
                .Setup(i => i.CopyTo(It.IsAny<Stream>( )));

            userInfoDto.SetProfilePicture(new FormFileTest( ));

            var response = await _sut.SignupAccountAsync(loginName, password, userInfoDto);

            Assert.Equal(201, response.StatusCode);
        }

        [Theory, AutoData]
        public async Task SignUpAndLogin_WhenNewUserCreatedAndLoggedIn_ResponseOk (string loginName, string password, UserInfoDto userInfoDto)
        {
            var account = default(Account);

            userInfoDto.SetProfilePicture(new FormFileTest( ));
            _accountsRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Account>( )))
                .Callback<Account>(u => account = u);

            _accountsRepositoryMock
                .Setup(a => a.GetByLoginAsync(loginName))
                .ReturnsAsync(( ) => null);

            _jwtServiceMock
                .Setup(j => j.GetJwtToken(It.IsAny<Account>( )))
                .Returns("token");

            var response = await _sut.SignupAccountAsync(loginName, password, userInfoDto);

            Assert.True(response.IsSuccess);

            _accountsRepositoryMock
                .Setup(r => r.GetByLoginAsync(loginName))
                .ReturnsAsync(account);

            var loginResponse = await _sut.LoginAsync(loginName, password);
            Assert.True(loginResponse.IsSuccess);
        }

        [Theory, TestUserAccount]
        public async Task GetUserInfoAsync_WhenFindUser_ReturnOk (Account account)
        {
            _accountsRepositoryMock
                .Setup(u => u.GetAsync(account.Id))
                .ReturnsAsync(account);

            var response = await _sut.GetUserInfoAsync(account.Id);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(account, response.Object);
            Assert.True(response.IsSuccess);
        }

        [Theory, TestUserAccount]
        public async Task GetUserInfoAsync_WhenNotFindUser_ReturnNotFind (Account account)
        {
            _accountsRepositoryMock
                .Setup(u => u.GetAsync(account.Id))
                .ReturnsAsync(( ) => null!);

            var response = await _sut.GetUserInfoAsync(account.Id);
            Assert.Null(response.Object);
            Assert.Equal(404, response.StatusCode);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task GetUsersAsync_WhenUserRoleAdministrator_ReturnOk ( )
        {
            var account = new TestAccount(UserRole.Admin);

            _accountsRepositoryMock
                .Setup(u => u.GetAsync(account.Id))
                .ReturnsAsync(account);

            _accountsRepositoryMock
                .Setup(u => u.GetAllAsync(It.IsAny<string>( )))
                .ReturnsAsync(new List<Account>
                {
                    new TestAccount(UserRole.User),
                    new TestAccount(UserRole.User),
                    new TestAccount(UserRole.User)
                });

            var response = await _sut.GetUsersAsync(account.Id, It.IsAny<string>( ));

            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            var usersCount = response.Object!.Count;
            Assert.Equal(3, usersCount);
        }

        [Fact]
        public async Task GetUsersAsync_WhenUserRoleNotAdministrator_ReturnNotAuthorized ( )
        {
            var account = new TestAccount(UserRole.User);

            _accountsRepositoryMock
                .Setup(u => u.GetAsync(account.Id))
                .ReturnsAsync(account);

            _accountsRepositoryMock
                .Setup(u => u.GetAllAsync(It.IsAny<string>( )))
                .ReturnsAsync(new List<Account>
                {
                    new TestAccount(UserRole.User),
                    new TestAccount(UserRole.User),
                    new TestAccount(UserRole.User)
                });

            var response = await _sut.GetUsersAsync(account.Id, It.IsAny<string>( ));

            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Null(response.Object);
        }

        [Fact]
        public async Task DeleteAccountAsync_WhenUserRoleAdmin_ReturnOk ( )
        {
            var adminAccount = new TestAccount(UserRole.Admin, generateGuid: true);
            var userAccount = new TestAccount(UserRole.User, generateGuid: true);

            _accountsRepositoryMock
                .Setup(u => u.GetAsync(adminAccount.Id))
                .ReturnsAsync(adminAccount);

            var response = await _sut.DeleteAccountAsync(adminAccount.Id, userAccount.Id);
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            _accountsRepositoryMock.Verify(v => v.DeleteAsync(userAccount.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountAsync_WhenUserTheSameUser_ReturnCannotDelete ( )
        {
            var adminAccount = new TestAccount(UserRole.Admin, generateGuid: true);
            var userAccount = new TestAccount(UserRole.User, generateGuid: true);

            _accountsRepositoryMock
                .Setup(u => u.GetAsync(adminAccount.Id))
                .ReturnsAsync(adminAccount);

            var response = await _sut.DeleteAccountAsync(adminAccount.Id, adminAccount.Id);
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            _accountsRepositoryMock.Verify(v => v.DeleteAsync(userAccount.Id), Times.Never);
        }

        [Fact]
        public async Task DeleteAccountAsync_WhenUserRoleNotAdmin_ReturnNotAuthorized ( )
        {
            var adminAccount = new TestAccount(UserRole.User);
            var userAccount = new TestAccount(UserRole.User);

            _accountsRepositoryMock
                .Setup(u => u.GetAsync(adminAccount.Id))
                .ReturnsAsync(adminAccount);

            var response = await _sut.DeleteAccountAsync(adminAccount.Id, userAccount.Id);
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            _accountsRepositoryMock.Verify(v => v.DeleteAsync(userAccount.Id), Times.Never);
        }
        [Theory, AutoData]
        public async Task UpdateUserInfoAsync_WhenUserInfoUpdated_AllUserPropertiesChanged (UserInfoDto userInfo)
        {
            var account = new TestAccount(UserRole.User);
        

            _accountsRepositoryMock
                .Setup(r => r.GetAsync(account.Id))
                .ReturnsAsync(account);


            var response = await _sut.UpdateUserInfoAsync(account.Id, userInfo);
            var changedUserInfo = response.Object!.UserInfo;

            Assert.Equal(userInfo.FirstName, changedUserInfo.FirstName);
            Assert.Equal(userInfo.LastName, changedUserInfo.LastName);
            Assert.Equal(userInfo.Phone, changedUserInfo.Phone);
            Assert.Equal(userInfo.PersonalCode, changedUserInfo.PersonalCode);
            Assert.Equal(userInfo.Email, changedUserInfo.Email);
            Assert.Equal(userInfo.City, changedUserInfo.Address.City.Name);
            Assert.Equal(userInfo.Street, changedUserInfo.Address.Street.Name);
            Assert.Equal(userInfo.HouseNumber, changedUserInfo.Address.HouseNumber);
            Assert.Equal(userInfo.AppartmentNumber, changedUserInfo.Address.AppartmentNumber);
        }

        [Fact]
        public async Task UpdateUserInfoAsync_WhenReceiveNulableProperties_AllUserPropertiesNotChanged ( )
        {
            var account = new TestAccount(UserRole.User);
            var userInfo = new UserInfoDto( );


            _accountsRepositoryMock
                .Setup(r => r.GetAsync(account.Id))
                .ReturnsAsync(account);

            var response = await _sut.UpdateUserInfoAsync(account.Id, userInfo);
            var changedUserInfo = response.Object!.UserInfo;

            Assert.Equal(account.UserInfo.FirstName, changedUserInfo.FirstName);
            Assert.Equal(account.UserInfo.LastName, changedUserInfo.LastName);
            Assert.Equal(account.UserInfo.Phone, changedUserInfo.Phone);
            Assert.Equal(account.UserInfo.PersonalCode, changedUserInfo.PersonalCode);
            Assert.Equal(account.UserInfo.Email, changedUserInfo.Email);
            Assert.Equal(account.UserInfo.Address.City, changedUserInfo.Address.City);
            Assert.Equal(account.UserInfo.Address.Street, changedUserInfo.Address.Street);
            Assert.Equal(account.UserInfo.Address.HouseNumber, changedUserInfo.Address.HouseNumber);
            Assert.Equal(account.UserInfo.Address.AppartmentNumber, changedUserInfo.Address.AppartmentNumber);
        }
    }
}
