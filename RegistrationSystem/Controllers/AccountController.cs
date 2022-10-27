using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.Extensions;

namespace RegistrationSystem.Controllers
{
    [Route("api/registration-system/user")]
    [ApiController]
    [DenyRoles( )]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo ( )
        {
            var response = await _accountService.GetUserInfoAsync(this.GetUserGuid( ));

            return StatusCode(response.StatuCode, new UserInfoResponse(response));
        }

    }
}
