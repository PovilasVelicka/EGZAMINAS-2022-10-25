using RegistrationSystem.BusinessLogic.DTOs;

namespace RegistrationSystem.Controllers.DTOs
{
    public class UserInfoDto : IUserInfoDto
    {
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? PersonalCode { get; }
        public string? Phone { get; }
        public string? Email { get; }
        public byte[ ]? Photo { get; }
        public string? City { get; }
        public string? Street { get; }
        public string? HouseNumber { get; }
        public string? AppartmentNumber { get; }
        public bool IsAllPropertiesNotEmpty ( )
        {
            return FirstName != null
                && LastName != null
                && PersonalCode != null
                && Phone != null
                && Email != null
                && City != null
                && Street != null
                && HouseNumber != null
                && AppartmentNumber != null;
        }

        public UserInfoDto ( ) { }

        public UserInfoDto (SignupRequest signupRequest)
        {
            FirstName = signupRequest.FirstName;
            LastName = signupRequest.LastName;
            PersonalCode = signupRequest.PersonalCode;
            Email = signupRequest.Email;
            City = signupRequest.City;
            Street = signupRequest.Street;
            HouseNumber = signupRequest.HouseNumber;
            AppartmentNumber = signupRequest.AppartmentNumber;
            Phone = signupRequest.Phone;
            using var memoryStream = new MemoryStream( );
            signupRequest.Image.CopyTo(memoryStream);
            Photo = memoryStream.ToArray( );
        }
    }
}
