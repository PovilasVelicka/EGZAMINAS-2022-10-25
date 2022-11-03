using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedInputStringAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid (
            object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Text cannot be empty");
            if (value is string val)
            {
                if (string.IsNullOrEmpty(val.Trim())) 
                    return new ValidationResult("Text cannot be empty");
            }
            return ValidationResult.Success;
        }
    }
}
