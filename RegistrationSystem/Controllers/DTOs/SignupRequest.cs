using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs
{
    
    public class SignupRequest
    {
        public string LoginName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalCode { get; set; } = null!;
        public string Phone { get; set; } = null!;

        [EmailValidate]
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string AppartmentNumber { get; set; } = null!;

        [AllowedExtensions(new string[ ] { ".jpg", ".jpeg", ".png", ".gif" })]
        [MaxFileSize(1024 * 1024  * 256)]
        public IFormFile Image { get; set; } = null!;
    }
}
