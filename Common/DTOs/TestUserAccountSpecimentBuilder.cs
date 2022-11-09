using AutoFixture.Kernel;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;
namespace Common.DTOs
{
    [SupportedOSPlatform("windows")]
    internal class TestUserAccountSpecimentBuilder : ISpecimenBuilder
    {
        public object Create (object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Account))
            {
                return new TestAccount(UserRole.User);
            }
            return new NoSpecimen( );
        }
    }
}
