using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedProfilePicturesAttribute : ValidationAttribute
    {
        private readonly string[ ] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly int _maxFileSize = 1024 * 1024;

        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extention = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extention.ToLower( )))
                {
                    return new ValidationResult(
                        $"Image type not supported, " +
                        $"supported types: {string.Join(", ", _allowedExtensions)}");
                }

                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(
                        $"The maximum allowed size of the uploaded image is 1Mb");
                }
            }

            return ValidationResult.Success;
        }
    }
}
