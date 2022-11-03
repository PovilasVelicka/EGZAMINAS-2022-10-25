using AutoFixture;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using Utilites.Exstensions;

namespace Common.DTOs
{
    [SupportedOSPlatform("windows")]
    public class TestAccount : Account
    {
        private readonly IFixture _fixture;
  
        public TestAccount (UserRole usreRole):base()
        {
            _fixture = new Fixture( );
            var password = _fixture.Create<string>();

            var (hash, salt) = password.CreatePasswordHash( );
            var accountId = Guid.NewGuid( );
            using var ms = new MemoryStream( );
            var image = new Bitmap(300, 300);
            image.Save(ms, ImageFormat.Png);

            LoginName = _fixture.Create<string>( );
            Id = accountId;
            PasswordHash = hash;
            PasswordSalt = salt;
            Role = usreRole;
            UserInfo = new UserInfo
            {
                Id = accountId,
                FirstName = _fixture.Create<string>( ),
                LastName = _fixture.Create<string>( ),
                Email = $"{_fixture.Create<string>( )}@mail.com",
                PersonalCode = _fixture.Create<string>( ),
                Phone = _fixture.Create<string>( ),
                Address = new Address
                {
                    Id = _fixture.Create<int>( ),
                    AppartmentNumber = _fixture.Create<int>( ).ToString(),
                    City = _fixture.Create<string>( ),
                    HouseNumber = _fixture.Create<int>( ).ToString( ),
                    Street =  _fixture.Create<string>( )
                },
                ProfilePicture = ms.ToArray( )
            };
        }
    }
}


