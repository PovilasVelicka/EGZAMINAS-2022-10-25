using RegistrationSystem.BusinessLogic.DTOs;

namespace NoteBook.Common.Interfaces.Services
{
    public interface IAuthService
    {
        /// <returns>If SignUp sucessfule return token else null </returns>
        Task<IServiceResponseDto<string>> SignupNewAccountAsync (string username, string password);

        /// <returns>If Login sucessfule return token else null </returns>
        Task<IServiceResponseDto<string>> LoginAsync (string username, string password);
    }
}
