using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class FirstNameRequest
    {
        [AllowedInputString]
        public string FirstName { get; set; } = null!;
        public FirstNameRequest ( )
        {

        }
    }
}
