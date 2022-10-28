using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedProfilePicturesAttribute : ValidationAttribute
    {
        private readonly string[ ] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly int _maxFileSize = 1024 * 1024 * 256;

        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extention = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extention.ToLower( )))
                {
                    return new ValidationResult($"Picture type is not supported, supproted types: {string.Join(", ", _allowedExtensions)}");
                }

                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed picture size {_maxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
