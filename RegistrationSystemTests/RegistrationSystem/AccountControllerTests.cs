using AutoFixture.Xunit2;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.DTOs.UserInfoRequestDto;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AccountController _sut;

        public AccountControllerTests ( )
        {
            _accountServiceMock = new Mock<IAccountService>( );
            _sut = new AccountController(_accountServiceMock.Object);
        }

        [Fact]
        public async Task GetUserInfo_ServiceRunTimeOnce ( )
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.GetUserInfoAsync(It.IsAny<Guid>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.GetUserInfo( );

            _accountServiceMock.Verify(s =>
            s.GetUserInfoAsync(It.IsAny<Guid>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateFirstName_ServiceRunTimeOnce (FirstNameRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateFirstName(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateLastName_ServiceRunTimeOnce (LastNameRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateLastName(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdatePersonalCode_ServiceRunTimeOnce (PersonalCodeRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdatePersonalCode(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdatePhoneNumber_ServiceRunTimeOnce (PhoneRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdatePhoneNumber(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateEmail_ServiceRunTimeOnce (EmailRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateEmail(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Fact]
        public async Task UpdateProfilePicture_ServiceRunTimeOnce ( )
        {
            SetRequestFakeUser("User");
            var profilePicture = new ProfilePictureRequest( ) { FormFile = new FormFileTest( ) };

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateProfilePicture(profilePicture);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateCity_ServiceRunTimeOnce (CityRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateCity(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateStreet_ServiceRunTimeOnce (StreetRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateStreet(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateHouseNumber_ServiceRunTimeOnce (HouseNumberRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateHouseNumber(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        [Theory, AutoData]
        public async Task UpdateAppartmentNumber_ServiceRunTimeOnce (AppartmentNumberRequest value)
        {
            SetRequestFakeUser("User");

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateAppartmentNumber(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        private void SetRequestFakeUser (string role)
        {
            var fakeUsser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[ ]
                    {
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.NameIdentifier,Guid.Empty.ToString())
                    })
                );

            _sut.ControllerContext = new ControllerContext( )
            {
                HttpContext = new DefaultHttpContext { User = fakeUsser }
            };
        }
    }
}
