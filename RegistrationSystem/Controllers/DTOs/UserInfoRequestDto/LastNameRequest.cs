using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class LastNameRequest
    {
        [AllowedInputString]
        public string LastName { get; set; } = null!;
        public LastNameRequest ( ) { }   
    }
}
