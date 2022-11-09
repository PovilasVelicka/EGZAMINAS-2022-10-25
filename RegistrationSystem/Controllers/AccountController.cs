using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.Services.AccountServices;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.DTOs.UserInfoRequestDto;
using RegistrationSystem.Controllers.Extensions;

namespace RegistrationSystem.Controllers
{
    [Route("api/registration-system")]
    [ApiController]
    [DenyRoles( )]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("user/details")]
        public async Task<IActionResult> GetUserInfo ( )
        {
            var response = await _accountService.GetUserInfoAsync(this.GetUserGuid( ));
            return this.MapServiceDto(response, new UserInfoResponse(response));
        }

        [HttpPatch("user/change/first-name")]
        public async Task<IActionResult> UpdateFirstName ([FromForm] FirstNameRequest firstNameRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { FirstName = firstNameRequest.FirstName });
            return this.MapServiceDto(response, response.Object?.UserInfo.FirstName.Value);
        }

        [HttpPatch("user/change/last-name")]
        public async Task<IActionResult> UpdateLastName ([FromForm] LastNameRequest lastNameRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { LastName = lastNameRequest.LastName });
            return this.MapServiceDto(response, response.Object?.UserInfo.LastName.Value);
        }

        [HttpPatch("user/change/personal-code")]
        public async Task<IActionResult> UpdatePersonalCode ([FromForm] PersonalCodeRequest personalCodeRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { PersonalCode = personalCodeRequest.PersonalCode });
            return this.MapServiceDto(response, response.Object?.UserInfo.PersonalCode.Value);
        }

        [HttpPatch("user/change/phone-number")]
        public async Task<IActionResult> UpdatePhoneNumber ([FromQuery] PhoneRequest phoneNumber)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Phone = phoneNumber.Phone });
            return this.MapServiceDto(response, response.Object?.UserInfo.Phone.Value);
        }

        [HttpPatch("user/change/email")]
        public async Task<IActionResult> UpdateEmail ([FromForm] EmailRequest emailRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Email = emailRequest.Email });
            return this.MapServiceDto(response, response.Object?.UserInfo.Email.Value );
        }

        [HttpPatch("user/change/profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture ([FromForm] ProfilePictureRequest profilePictureRequest)
        {
            var userInfoDto = new UserInfoDto( );
            userInfoDto.SetProfilePicture(profilePictureRequest.FormFile);

            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), userInfoDto);
            return this.MapServiceDto(response, response.Object?.UserInfo.ProfilePicture);
        }

        [HttpPatch("user/change/city")]
        public async Task<IActionResult> UpdateCity ([FromForm] CityRequest cityRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { City = cityRequest.City });
            return this.MapServiceDto(response, response.Object?.UserInfo.Address.City.Value);
        }

        [HttpPatch("user/change/street")]
        public async Task<IActionResult> UpdateStreet ([FromForm] StreetRequest streetRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { Street = streetRequest.Street });
            return this.MapServiceDto(response, response.Object?.UserInfo.Address.Street.Value);
        }

        [HttpPatch("user/change/house-number")]
        public async Task<IActionResult> UpdateHouseNumber ([FromForm] HouseNumberRequest houseNumberRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { HouseNumber = houseNumberRequest.HouseNumber });
            return this.MapServiceDto(response, response.Object?.UserInfo.Address.HouseNumber.Value);
        }

        [HttpPatch("user/change/appartment-number")]
        public async Task<IActionResult> UpdateAppartmentNumber ([FromForm] AppartmentNumberRequest appartmentNumberRequest)
        {
            var response = await _accountService.UpdateUserInfoAsync(this.GetUserGuid( ), new UserInfoDto { AppartmentNumber = appartmentNumberRequest.AppartmentNumber });
            return this.MapServiceDto(response, response.Object?.UserInfo.Address.AppartmentNumber.Value);
        }
    }
}
