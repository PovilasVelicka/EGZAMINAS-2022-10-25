using RegistrationSystem.Controllers.DTOs.UserInfoRequestDto;
using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs
{

    public class SignupRequest
    {
        [AllowedInputString]
        public string LoginName { get; set; } = null!;
        [AllowedInputString]
        public string Password { get; set; } = null!;
        [AllowedInputString]
        public string FirstName { get; set; } = null!;
        [AllowedInputString]
        public string LastName { get; set; } = null!;
        [AllowedInputString]
        public string PersonalCode { get; set; } = null!;
        [AllowedInputString]
        public string Phone { get; set; } = null!;
        [AllowedEmails]
        public string Email { get; set; } = null!;
        [AllowedInputString]
        public string City { get; set; } = null!;
        [AllowedInputString]
        public string Street { get; set; } = null!;
        [AllowedInputString]
        public string HouseNumber { get; set; } = null!;
        [AllowedInputString]
        public string AppartmentNumber { get; set; } = null!;
        [AllowedProfilePictures]
        public IFormFile ProfilePicture { get; set; } = null!;
    }
}
