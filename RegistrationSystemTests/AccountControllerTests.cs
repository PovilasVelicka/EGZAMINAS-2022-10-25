using Moq;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers;
using System.Runtime.Versioning;

namespace RegistrationSystemTests
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



    }
}
