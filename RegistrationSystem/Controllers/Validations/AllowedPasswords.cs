using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Controllers.Validations
{
    public class AllowedPasswordsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                var capitalExists = password.ToCharArray( ).Where(c => char.IsUpper(c)).Any( );
                var lowerExists = password.ToCharArray( ).Where(c => char.IsLower(c)).Any( );
                var digitsExists = password.ToCharArray( ).Where(c => char.IsDigit(c)).Any( );
                if (!capitalExists
                 || !lowerExists
                 || !digitsExists
                 || password.ToCharArray( ).Length < 6)
                    return new ValidationResult(
                        "Invalid password structure. " +
                        "Password must contain uppercase, " +
                        "lowercase and numeric characters");
            }
            return ValidationResult.Success;
        }
    }
}
