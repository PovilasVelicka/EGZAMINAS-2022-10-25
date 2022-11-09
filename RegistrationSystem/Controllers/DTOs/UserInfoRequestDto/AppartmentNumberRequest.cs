using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class AppartmentNumberRequest
    {
        [AllowedInputString]
        public string AppartmentNumber { get; set; } = null!;
        public AppartmentNumberRequest ( )
        {

        }
    }
}
