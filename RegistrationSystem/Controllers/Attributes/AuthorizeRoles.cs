using Microsoft.AspNetCore.Authorization;
using RegistrationSystem.Entities.Enums;
using System.Data;

namespace RegistrationSystem.Controllers.Attributes
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles (params UserRole[ ] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(UserRole), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }

    public class DenyRoles : AuthorizeAttribute
    {
        public DenyRoles (params UserRole[ ] roles)
        {
            var existsRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>( );

            var allowedRolesAsStrings =
                existsRoles
                .Where(r => !roles.Any(d => d != r))
                .Select(x => Enum.GetName(typeof(UserRole), x));

            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
