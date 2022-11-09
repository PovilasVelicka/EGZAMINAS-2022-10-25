using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.Controllers.DTOs
{
    public class AdminUserInfoResponse
    {
        public Guid Id { get; }
        public string LoginName { get; } = null!;
        public UserRole Role { get; }
        public string FirstName { get; } = null!;
        public string LastName { get; } = null!;
        public string Phone { get; } = null!;
        public string Email { get; } = null!;
        public AdminUserInfoResponse ( ) { }
        public AdminUserInfoResponse (Account account)
        {
            Id = account.Id;
            LoginName = account.LoginName;
            Role = account.Role;
            FirstName = account.UserInfo.FirstName.Value;
            LastName = account.UserInfo.LastName.Value;
            Phone = account.UserInfo.Phone.Value;
            Email = account.UserInfo.Email.Value;
        }
    }
}
