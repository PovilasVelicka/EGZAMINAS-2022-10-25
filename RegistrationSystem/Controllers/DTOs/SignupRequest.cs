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
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string AppartmentNumber { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
