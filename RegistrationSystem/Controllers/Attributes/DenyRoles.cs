using Microsoft.AspNetCore.Authorization;
using RegistrationSystem.Entities.Enums;
using System.Data;

namespace RegistrationSystem.Controllers.Attributes
{
    public class DenyRoles : AuthorizeAttribute
    {
        public DenyRoles (params UserRole[ ] roles)
        {
            var existsRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>( );

            var allowedRolesAsStrings =
                existsRoles
                    .Where(r => roles.All(rr => rr != r))
                    .Select(x => Enum.GetName(typeof(UserRole), x))
                    .ToArray();

            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
