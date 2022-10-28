using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class HouseNumberRequest
    {
        [AllowedInputString]
        public string HouseNumber { get; set; } = null!;
        public HouseNumberRequest ( )
        {

        }
    }
}
