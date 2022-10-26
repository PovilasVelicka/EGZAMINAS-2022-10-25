namespace RegistrationSystem.Controllers.DTOs
{
    public class SignupRequest 
    {
        public string LoginName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; } = string.Empty;

        public string LastName { get; } = string.Empty;

        public string PersonalCode { get; } = string.Empty;

        public string Phone { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string City { get; } = string.Empty;

        public string Street { get; } = string.Empty;

        public string HouseNumber { get; } = string.Empty;

        public string AppartmentNumber { get; } = string.Empty;

        public IFormFile Image { get; set; } = null!;
    }
}
