using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class PhoneRequest
    {
        [AllowedInputString]
        public string Phone { get; set; } = null!;
        public PhoneRequest ( )
        {

        }
    }
}
