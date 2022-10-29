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
}
