using AutoFixture;
using AutoFixture.Xunit2;
using Common.DTOs;
using System.Runtime.Versioning;

namespace Common.Attributes
{
    public class TestUserAccountAttribute : AutoDataAttribute
    {
        [SupportedOSPlatform("windows")]
        public TestUserAccountAttribute ( ) : base(( ) =>
        {
            var fixture = new Fixture( );
            fixture.Customizations.Add(new TestUserAccountSpecimentBuilder( ));
            return fixture;
        }){ }
    }
}
