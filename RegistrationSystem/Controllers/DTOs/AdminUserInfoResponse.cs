using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.IO;

namespace RegistrationSystem.Controllers.DTOs
{
    public class AdminUserInfoResponse
    {
        public Guid Id { get;  }
        public string LoginName { get; } = null!;
        public UserRole Role { get; }
        public string FirstName { get; } = null!;
        public string LastName { get; } = null!;
        public string Phone { get; } = null!;
        public string Email { get; } = null!;
        public AdminUserInfoResponse ( ) { }
        public AdminUserInfoResponse (Account responseDto)
        {
            Id = responseDto.Id;
            LoginName = responseDto.LoginName;
            Role = responseDto.Role;
            FirstName = responseDto.UserInfo.FirstName;
            LastName = responseDto.UserInfo.LastName;          
            Phone = responseDto.UserInfo.Phone;
            Email = responseDto.UserInfo.Email;           
        }
    }
}
