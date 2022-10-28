using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[ ] _allowedExtensions;
        public AllowedExtensionsAttribute (string[ ] extensions)
        {
            _allowedExtensions = extensions;
        }

        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extention = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extention.ToLower( )))
                {
                    return new ValidationResult($"Foto type is not supported, supproted types: {string.Join("; ", _allowedExtensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
