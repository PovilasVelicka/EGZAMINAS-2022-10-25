using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class PersonPictureRequest
    {
        [AllowedProfilePictures]
        public IFormFile FormFile { get; set; } = null!;
        public PersonPictureRequest() { }
    }
}
