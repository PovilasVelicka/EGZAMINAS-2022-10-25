using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class EmailRequest
    {
        [AllowedEmails]
        public string Email { get; set; } = null!;
        public EmailRequest ( ) { }
    }
}
