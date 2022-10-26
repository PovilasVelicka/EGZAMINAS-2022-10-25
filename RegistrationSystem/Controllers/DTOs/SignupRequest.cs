namespace RegistrationSystem.Controllers.DTOs
{
    public class SignupRequest 
    {
        public string LoginName { get; set; } = null!;

        public string Password { get; set; } = null!;
       
        public string FirstName { get; } = null!;

        public string LastName { get; } = null!;

        public string PersonalCode { get; } = null!;

        public string Phone { get; } = null!;

        public string Email { get; } = null!;       

        public string City { get; } = null!;

        public string Street { get; } = null!;

        public string HouseNumber { get; } = null!;

        public string AppartmentNumber { get; } = null!;

        public IFormFile Image { get; set; } = null!;
    }
}
