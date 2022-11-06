using AutoFixture;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;
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
        private static int _addressId = 0;
        public TestAccount (UserRole usreRole, bool generateGuid = false) : base( )
        {
            _fixture = new Fixture( );
            var password = _fixture.Create<string>( );

            var (hash, salt) = password.CreatePasswordHash( );
            var accountId = generateGuid ? Guid.NewGuid( ) : Guid.Empty;
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
                FirstName = new FirstName { Value = _fixture.Create<string>( ) },
                LastName = new LastName { Value = _fixture.Create<string>( ) },
                Email = new Email { Value = $"{_fixture.Create<string>( )}@mail.com" },
                PersonalCode = new PersonalCode { Value = _fixture.Create<string>( ) },
                Phone = new Phone { Value = _fixture.Create<string>( ) },
                Address = new Address
                {
                    Id = ++_addressId,
                    AppartmentNumber = new AppartmentNumber { Value = _fixture.Create<int>( ).ToString( ) },
                    City = new City { Value = _fixture.Create<string>( ) },
                    HouseNumber = new HouseNumber { Value = _fixture.Create<int>( ).ToString( ) },
                    Street = new Street { Value = _fixture.Create<string>( ) },
                },

                ProfilePicture = ms.ToArray( )
            };
        }
    }
}


