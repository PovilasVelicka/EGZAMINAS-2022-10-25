using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.Extensions;

namespace RegistrationSystem.Controllers
{
    [Route("api/registration-system")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AuthController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync ([FromForm] LoginRequest loginRequest)
        {
            var response = await _accountService.LoginAsync(loginRequest.LoginName, loginRequest.Password);

            return this.MapServiceDto(response, response.Object);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync ([FromForm] SignupRequest signupRequest)
        {
            var userDto = new UserInfoDto(signupRequest);

            var response = await _accountService.SignupAccountAsync(signupRequest.LoginName, signupRequest.Password, userDto);

            return this.MapServiceDto(response, response.Object);
        }
    }
}
