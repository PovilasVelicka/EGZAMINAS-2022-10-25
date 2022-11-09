using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.Extensions;
using RegistrationSystem.Entities.Enums;

namespace RegistrationSystem.Controllers
{
    [Route("api/registration-system")]
    [ApiController]
    [AuthorizeRoles(UserRole.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AdminController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("admin/users/{searchText}")]
        public async Task<IActionResult> GetUsers (string searchText)
        {
            var response = await _accountService.GetUsersAsync(this.GetUserGuid( ), searchText);
            var users = response.Object?.Select(u => new AdminUserInfoResponse(u)).ToList( );
            return this.MapServiceDto(response, users);
        }

        [HttpDelete("admin/delete-user")]
        public async Task<IActionResult> DeleteUser ([FromForm] Guid userGuid)
        {
            var response = await _accountService.DeleteAccountAsync(this.GetUserGuid( ), userGuid);
            return this.MapServiceDto(response, response.Object);
        }
    }
}
