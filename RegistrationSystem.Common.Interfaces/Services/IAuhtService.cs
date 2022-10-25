namespace NoteBook.Common.Interfaces.Services
{
    public interface IAuthService
    {
        /// <returns>If SignUp sucessfule return token else null </returns>
        Task<string> SignupNewAccountAsync (string username, string password, byte[ ] picture);

        /// <returns>If Login sucessfule return token else null </returns>
        Task<string> LoginAsync (string username, string password);
    }
}
