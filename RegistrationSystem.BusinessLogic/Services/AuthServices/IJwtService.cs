using RegistrationSystem.Entities.Models;

namespace NoteBook.BusinessLogic.Services.AuthServices
{
    public interface IJwtService
    {
        string GetJwtToken (Account accont);
    }
}
