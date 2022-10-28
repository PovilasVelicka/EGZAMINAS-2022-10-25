using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.DTOs.UserInfoRequestDto;
using RegistrationSystem.Controllers.Extensions;
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
        public async Task<IActionResult> UpdateFirstName ([FromForm] FirstNameRequest firstNameRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { FirstName = firstNameRequest.FirstName });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/last-name")]
        public async Task<IActionResult> UpdateLastName ([FromForm] LastNameRequest lastNameRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { LastName = lastNameRequest.LastName });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/personal-code")]
        public async Task<IActionResult> UpdatePersonalCode ([FromForm] PersonalCodeRequest personalCodeRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { PersonalCode = personalCodeRequest.PersonalCode });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/phone-number")]
        public async Task<IActionResult> UpdatePhoneNumber ([FromQuery] PhoneRequest phoneNumber)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Phone = phoneNumber.Phone });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/email")]
        public async Task<IActionResult> UpdateEmail ([FromForm] EmailRequest emailRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Email = emailRequest.Email });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture ([FromForm] PersonPictureRequest profilePictureRequest)
        {
            var userInfoDto = new UserInfoDto( );
            userInfoDto.SetProfilePicture(profilePictureRequest.FormFile);

            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), userInfoDto);
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/city")]
        public async Task<IActionResult> UpdateCity ([FromForm] CityRequest cityRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { City = cityRequest.City });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/street")]
        public async Task<IActionResult> UpdateStreet ([FromForm] StreetRequest streetRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Street = streetRequest.Street });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/house-number")]
        public async Task<IActionResult> UpdateHouseNumber ([FromForm] HouseNumberRequest houseNumberRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { HouseNumber = houseNumberRequest.HouseNumber });
            return StatusCode(response.StatuCode, response.Message);
        }

        [HttpPatch("change/appartment-number")]
        public async Task<IActionResult> UpdateAppartmentNumber ([FromForm] AppartmentNumberRequest appartmentNumberRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { AppartmentNumber = appartmentNumberRequest.AppartmentNumber });
            return StatusCode(response.StatuCode, response.Message);
        }

        #endregion
    }
}
