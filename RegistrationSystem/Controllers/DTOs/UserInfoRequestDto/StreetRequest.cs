using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class StreetRequest
    {
        [AllowedInputString]
        public string Street { get; set; } = null!;
        public StreetRequest ( )
        {

        }
    }
}
