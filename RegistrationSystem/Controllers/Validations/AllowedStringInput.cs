using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedInputStringAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is string val)
            {
                if (string.IsNullOrEmpty(val)) return new ValidationResult("Text cannot be empty");
            }
            return ValidationResult.Success;
        }
    }
}
