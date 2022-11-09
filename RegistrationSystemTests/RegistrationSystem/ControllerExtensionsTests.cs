using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers;
using RegistrationSystem.Controllers.Extensions;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class ControllerExtensionsTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AdminController _sut;
        public ControllerExtensionsTests ( )
        {
            _accountServiceMock = new Mock<IAccountService>( );
            _sut = new AdminController(_accountServiceMock.Object);
        }

        [Fact]
        public void GetUserGuid_ReturnUserGuid ( )
        {
            var expected = Guid.NewGuid( );
            SetFakeUser("Admin", expected);
            var actual = ControllerBaseExeption.GetUserGuid(_sut);

            Assert.Equal(expected, actual);
        }

        private void SetFakeUser (string role, Guid id)
        {
            var fakeUsser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[ ]
                    {
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.NameIdentifier,id.ToString())
                    })
                );

            _sut.ControllerContext = new ControllerContext( )
            {
                HttpContext = new DefaultHttpContext { User = fakeUsser }
            };
        }
    }
}
