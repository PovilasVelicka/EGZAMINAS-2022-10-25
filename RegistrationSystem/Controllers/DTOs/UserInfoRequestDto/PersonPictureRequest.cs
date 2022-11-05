using RegistrationSystem.Controllers.Validations;

namespace RegistrationSystem.Controllers.DTOs.UserInfoRequestDto
{
    public class ProfilePictureRequest
    {
        [AllowedProfilePictures]
        public IFormFile FormFile { get; set; } = null!;
        public ProfilePictureRequest ( ) { }
    }
}
