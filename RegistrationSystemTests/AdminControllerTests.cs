using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace RegistrationSystemTests
{
    [SupportedOSPlatform("windows")]
    public class AdminControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AdminController _sut;
        public AdminControllerTests ( )
        {
            _accountServiceMock = new Mock<IAccountService>( );
            _sut = new AdminController(_accountServiceMock.Object);
        }


        [Fact]
        public async Task GetUsers_WhenUserAdmin_ReturnOk ( )
        {
            SetFakeUser("Admin");

            var testAccounts = new List<Account>( );
            for (int i = 0; i < 10; i++)
            {
                testAccounts.Add(new TestAccount(UserRole.User, generateGuid: true));
            }

            _accountServiceMock
                .Setup(s => s.GetUsersAsync(It.IsAny<Guid>( ), It.IsAny<string>( )))
                .ReturnsAsync(new ServiceResponseDto<List<Account>>(testAccounts));

            await _sut.GetUsers("emptyString");
            _accountServiceMock.Verify(s =>
           s.GetUsersAsync(It.IsAny<Guid>( ), It.IsAny<string>( )), Times.Once( ));

        }


        [Fact]
        public async Task DeleteUser_WehenDelete_ReturnOk ( )
        {
            SetFakeUser("Admin");

            _accountServiceMock
                .Setup(s => s.DeleteAccountAsync(It.IsAny<Guid>( ), It.IsAny<Guid>( )))
                .ReturnsAsync(new ServiceResponseDto<string>("deleted", true));

            await _sut.DeleteUser(Guid.NewGuid( ));

            _accountServiceMock.Verify(s =>
            s.DeleteAccountAsync(It.IsAny<Guid>( ), It.IsAny<Guid>( )), Times.Once( ));
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
