using AutoFixture.Xunit2;
using RegistrationSystem.Controllers.Attributes;
using RegistrationSystem.Entities.Enums;
using System.Runtime.Versioning;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class ControllerAttributesTests
    {

        [Fact]
        public void AuthorizeRolesAttribute_WhenParamsHasRoles_ReturnSeparatedStringAllowedRoles()
        {
            var attribute = new AuthorizeRoles(new UserRole[] { UserRole.Admin, UserRole.User });
            var expected = "Admin,User";
            var actual = attribute.Roles;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineAutoData]
        [InlineAutoData]
        [InlineAutoData]
        [InlineData(new UserRole[] { })]
        public void DenyRolesAttribute_WhenParamsHasRoles_ReturnSeparatedStringAllowedRoles(UserRole[] userRoles)
        {
            if (userRoles == null) userRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToArray();

            var attributre = new DenyRoles(userRoles);
            var expected = Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Where(r => userRoles.All(rr => rr != r))
                .ToArray();

            var actual = attributre
                .Roles?
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(r => (UserRole)Enum.Parse(typeof(UserRole), r))
                .ToArray() ?? new UserRole[] { };

            var isUserRoleExists = expected.All(r => actual.Any(rr => rr == r));
            Assert.True(isUserRoleExists);
        }
    }
}
