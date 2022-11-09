using System.ComponentModel.DataAnnotations;
using Utilites.Exstensions;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedEmailsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                if (!email.IsValidEmail( ))
                {
                    return new ValidationResult($"Email {email} not valid");
                }
            }

            return ValidationResult.Success;
        }
    }
}
