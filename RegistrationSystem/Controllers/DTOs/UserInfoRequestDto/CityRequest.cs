using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class CityRequest
    {
        [AllowedInputString]
        public string City { get; set; } = null!;
        public CityRequest ( )
        {

        }
    }
}
