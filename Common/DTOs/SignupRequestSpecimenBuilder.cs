using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Http;
using Moq;
using RegistrationSystem.Controllers.DTOs;
using System.Runtime.Versioning;

namespace Common.DTOs
{
    [SupportedOSPlatform("windows")]
    public class SignupRequestSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Mock<IFormFile> _iFormFileMock;
        public SignupRequestSpecimenBuilder ( )
        {
            _iFormFileMock = new Mock<IFormFile> ();
        }

       
  
        public object Create (object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SignupRequest))
            {
                using var stream = new MemoryStream( );
                _iFormFileMock
                    .Setup(f => f.CopyTo(stream));
                   

                return new SignupRequest
                {
                    LoginName = "LoginName",
                    Password = "LoginNamePassword123",
                    FirstName = "FirstLoginName",
                    LastName = "LastLoginName",
                    Email = "loginname@gmail.com",
                    Phone = "37069553298",
                    PersonalCode = "37501050000",
                    City = "Klaipeda",
                    Street = "Smilteles",
                    AppartmentNumber = "11",
                    HouseNumber = "--",
                    ProfilePicture = _iFormFileMock.Object
                };
            }
            return new NoSpecimen( );
        }
    }
}
