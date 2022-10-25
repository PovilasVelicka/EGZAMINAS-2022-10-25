using RegistrationSystem.Entities.Models;

namespace NoteBook.Common.Interfaces.Services
{
    public interface IJwtService
    {
        string GetJwtToken (Account accont);
    }
}
