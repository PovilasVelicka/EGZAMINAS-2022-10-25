using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;

namespace RegistrationSystem.Controllers.DTOs
{
    public class UserInfoResponse
    {
        public string LoginName { get; } = null!;
        public UserRole Role { get; }
        public string FirstName { get; } = null!;
        public string LastName { get; } = null!;
        public string PersonalCode { get; } = null!;
        public string Phone { get; } = null!;
        public string Email { get; } = null!;
        public string City { get; } = null!;
        public string Street { get; } = null!;
        public string HouseNumber { get; } = null!;
        public string AppartmentNumber { get; } = null!;
        public byte[ ] ProfilePicture { get; } = null!;

        public UserInfoResponse ( ) { }
        public UserInfoResponse (IServiceResponseDto<Account> responseDto)
        {
            var obj = responseDto.Object;
            if (obj == null) return;

            LoginName = obj.LoginName;
            Role = obj.Role;
            FirstName = obj.UserInfo.FirstName.Value;
            LastName = obj.UserInfo.LastName.Value;
            PersonalCode = obj.UserInfo.PersonalCode.Value;
            Phone = obj.UserInfo.Phone.Value;
            Email = obj.UserInfo.Email.Value;
            City = obj.UserInfo.Address.City.Value;
            Street = obj.UserInfo.Address.Street.Value;
            HouseNumber = obj.UserInfo.Address.HouseNumber.Value;
            AppartmentNumber = obj.UserInfo.Address.AppartmentNumber.Value;
            ProfilePicture = obj.UserInfo.ProfilePicture;
        }
    }
}
