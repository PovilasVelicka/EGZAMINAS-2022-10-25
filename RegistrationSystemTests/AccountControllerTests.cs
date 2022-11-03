using AutoFixture;
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

namespace RegistrationSystemTests
{
    [SupportedOSPlatform("windows")]
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AccountController _sut;
        private readonly IFixture _fixture;
        public AccountControllerTests ( )
        {
            _accountServiceMock = new Mock<IAccountService>( );
            _sut = new AccountController(_accountServiceMock.Object);
            _fixture = new Fixture( );
        }

        [Fact]
        public async Task GetUserInfo_ServiceRunTimeOnce ( )
        {
            SetFakeUser("User");

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
            SetFakeUser("User");
            var userInfo = new UserInfoDto { FirstName = value.FirstName };

            _accountServiceMock
                .Setup(s => s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>()))
                .ReturnsAsync(new ServiceResponseDto<Account>(new TestAccount(UserRole.User)));

            await _sut.UpdateFirstName(value);

            _accountServiceMock.Verify(s =>
            s.UpdateUserInfoAsync(It.IsAny<Guid>( ), It.IsAny<UserInfoDto>( )), Times.Once( ));
        }

        private void SetFakeUser (string role)
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
