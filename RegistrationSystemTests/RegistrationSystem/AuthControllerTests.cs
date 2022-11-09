using AutoFixture;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers;
using RegistrationSystem.Controllers.DTOs;
using System.Net;
using System.Runtime.Versioning;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class AuthControllerTests
    {

        private readonly Mock<IAccountService> _accontServiceMock;
        private readonly AuthController _sut;
        private readonly IFixture _fixture;
        public AuthControllerTests()
        {
            _accontServiceMock = new Mock<IAccountService>();
            _sut = new AuthController(_accontServiceMock.Object);
            _fixture = new Fixture();
            _fixture.Customizations.Add(new SignupRequestSpecimenBuilder());
        }

        [Fact]
        public async Task Login_WhenUserNotExists_ResponseNotFound()
        {
            var user = _fixture.Create<LoginRequest>();
            _accontServiceMock
                .Setup(u => u.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ServiceResponseDto<string>(null, "User name not exists", (int)HttpStatusCode.NotFound));

            var response = _sut.LoginAsync(user);
            var actual = await response as ObjectResult;
            Assert.Equal(404, actual!.StatusCode);
        }

        [Fact]
        public async Task Login_WhenWrongPassword_ResponseUnauthorized()
        {
            var user = _fixture.Create<LoginRequest>();
            _accontServiceMock
                .Setup(u => u.LoginAsync(user.LoginName, It.IsAny<string>()))
                .ReturnsAsync(new ServiceResponseDto<string>(null, "Incorrect password", (int)HttpStatusCode.Unauthorized));

            var response = _sut.LoginAsync(user);
            var actual = await response as ObjectResult;
            Assert.Equal(401, actual!.StatusCode);
        }

        [Fact]
        public async Task Login_WhenUserExistsAndPasswordCorrect_ResponseOk()
        {
            var user = _fixture.Create<LoginRequest>();
            _accontServiceMock
                .Setup(u => u.LoginAsync(user.LoginName, user.Password))
                .ReturnsAsync(new ServiceResponseDto<string>("token", "Login succesfull", (int)HttpStatusCode.OK));

            var response = _sut.LoginAsync(user);
            var actual = await response as ObjectResult;
            Assert.NotNull(actual);
            Assert.Equal(200, actual!.StatusCode);
        }

        [Fact]
        public async Task SignupAccountAsync_WhenNewUser_CreateNewUser()
        {
            var signupRequest = _fixture.Create<SignupRequest>();

            _accontServiceMock
                .Setup(s => s.SignupAccountAsync(signupRequest.LoginName, signupRequest.Password, It.IsAny<UserInfoDto>()))
                .ReturnsAsync(new ServiceResponseDto<string>(
                "token",
                "Account created successfuly",
                (int)HttpStatusCode.Created));

            var response = _sut.SignUpAsync(signupRequest);
            var actual = await response as ObjectResult;

            Assert.NotNull(actual);
            Assert.Equal(201, actual!.StatusCode);

            _accontServiceMock.Verify(m =>
            m.SignupAccountAsync(
                signupRequest.LoginName,
                signupRequest.Password,
                It.IsAny<UserInfoDto>()
                ), Times.Once);
        }


    }
}
