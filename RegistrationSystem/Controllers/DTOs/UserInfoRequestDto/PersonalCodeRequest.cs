using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class PersonalCodeRequest
    {
        [AllowedInputString]
        public string PersonalCode { get; set; } = null!;
        public PersonalCodeRequest ( ) { }
    }
}
