using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.Extensions;
using RegistrationSystem.Controllers.Validations;
using RegistrationSystem.Entities.Enums;

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

        #region Admin end points

        [HttpGet("admin/users/{searchText}")]
        [AuthorizeRoles(UserRole.Admin)]
        public async Task<IActionResult> GetUsers (string searchText)
        {
            var response = await _accountService.GetUsersAsync(this.GetUserGuid( ), searchText);
            var users = response.Object?.Select(u => new AdminUserInfoResponse(u));
            return StatusCode(response.StatuCode, users);
        }

        [HttpDelete("admin/delete-user")]
        [AuthorizeRoles(UserRole.Admin)]
        public async Task<IActionResult> DeleteUser ([FromForm] Guid userGuid)
        {
            var response = await _accountService.DeleteAccountAsync(this.GetUserGuid( ), userGuid);
            return StatusCode(response.StatuCode, response.Message);
        }

        #endregion

        #region User end points

        [HttpGet("details")]
        public async Task<IActionResult> GetUserInfo ( )
        {
            var response = await _accountService.GetUserInfoAsync(this.GetUserGuid( ));
            return StatusCode(response.StatuCode, new UserInfoResponse(response));
        }

        [HttpPatch("change/first-name")]
        public async Task<IActionResult> UpdateFirstName ([FromForm] string firstName)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { FirstName = firstName });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/last-name")]
        public async Task<IActionResult> UpdateLastName ([FromForm] string lastName)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { LastName = lastName });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/personal-code")]
        public async Task<IActionResult> UpdatePersonalCode ([FromForm] string personalCode)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { PersonalCode = personalCode });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/phone-number")]
        public async Task<IActionResult> UpdatePhoneNumber ([FromForm] string phoneNumber)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Phone = phoneNumber });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/email")]
        [EmailValidate]
        public async Task<IActionResult> UpdateEmail ([FromForm] string email)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Email = email });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/profile-picture")]
        [AllowedExtensions(new string[ ] { ".jpg", ".jpeg", ".png", ".gif" })]
        [MaxFileSize(1024 * 1024 * 256)]
        public async Task<IActionResult> UpdateProfilePicture (IFormFile profilePicture)
        {
            var userInfoDto = new UserInfoDto( );
            userInfoDto.SetProfilePicture(profilePicture);

            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), userInfoDto);
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/city")]
        public async Task<IActionResult> UpdateCity ([FromForm] string city)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { City = city });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/street")]
        public async Task<IActionResult> UpdateStreet ([FromForm] string street)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Street = street });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/house-number")]
        public async Task<IActionResult> UpdateHouseNumber ([FromForm] string houseNumber)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { HouseNumber = houseNumber });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/appartment-number")]
        public async Task<IActionResult> UpdateAppartmentNumber ([FromForm] string appartmentNumber)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { AppartmentNumber = appartmentNumber });
            return StatusCode(response.StatuCode, response.Message);
        }

        #endregion
    }
}
