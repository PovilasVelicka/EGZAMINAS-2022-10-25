using RegistrationSystem.BusinessLogic.DTOs;

namespace RegistrationSystem.Controllers.DTOs
{
    public class UserInfoDto : IUserInfoDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public byte[ ]? Photo { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? AppartmentNumber { get; set; }
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
