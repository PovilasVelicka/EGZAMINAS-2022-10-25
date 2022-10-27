using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RegistrationSystem.Controllers.Extensions
{
    internal static class ControllerBaseExeption
    {
        public static Guid GetUserGuid (this ControllerBase controller)
        {
            var userIdClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) { return Guid.Empty; }

            Guid.TryParse(userIdClaim.Value, out Guid UserId);
            return UserId;
        }
    }
}
