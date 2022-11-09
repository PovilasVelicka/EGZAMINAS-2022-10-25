using AutoFixture;
using AutoFixture.Xunit2;
using Common.Attributes;
using Common.DTOs;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class DTOsTests
    {
        private readonly IFixture _fixture;
        public DTOsTests ( )
        {
            _fixture = new Fixture( );
        }
        [Theory, TestUserAccount]
        public void UserInfoResponse_WhenNewWithAccount_AllFieldsUpdated (Account account)
        {
            var serviceResponseDto = new ServiceResponseDto<Account>(account);     

            var expected = serviceResponseDto.Object!;

            var actual = new UserInfoResponse(serviceResponseDto);

            Assert.Equal(expected.LoginName, actual.LoginName);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.UserInfo.PersonalCode.Value, actual.PersonalCode);
            Assert.Equal(expected.UserInfo.FirstName.Value, actual.FirstName);
            Assert.Equal(expected.UserInfo.LastName.Value, actual.LastName);
            Assert.Equal(expected.UserInfo.Phone.Value, actual.Phone);
            Assert.Equal(expected.UserInfo.Email.Value, actual.Email);
            Assert.Equal(expected.UserInfo.ProfilePicture, actual.ProfilePicture);
            Assert.Equal(expected.UserInfo.Address.City.Value, actual.City);
            Assert.Equal(expected.UserInfo.Address.Street.Value, actual.Street);
            Assert.Equal(expected.UserInfo.Address.HouseNumber.Value, actual.HouseNumber);
            Assert.Equal(expected.UserInfo.Address.AppartmentNumber.Value, actual.AppartmentNumber);
        }

        [Theory, TestUserAccount]
        public void AdminUserInfoResponse_WhenNewWithAccount_AllFieldsUpdated (Account account)
        {
            var expected = account;

            var actual = new AdminUserInfoResponse(account);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.LoginName, actual.LoginName);
            Assert.Equal(expected.Role, actual.Role);          
            Assert.Equal(expected.UserInfo.FirstName.Value, actual.FirstName);
            Assert.Equal(expected.UserInfo.LastName.Value, actual.LastName);
            Assert.Equal(expected.UserInfo.Phone.Value, actual.Phone);
            Assert.Equal(expected.UserInfo.Email.Value, actual.Email);
        }
    }
}
