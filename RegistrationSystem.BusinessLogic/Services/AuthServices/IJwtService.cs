using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.BusinessLogic.Services.AuthServices
{
    public interface IJwtService
    {
        string GetJwtToken (Account accont);
    }
}
