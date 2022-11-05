using Common.Attributes;
using Common.DTOs;
using RegistrationSystem.Controllers.DTOs;
using RegistrationSystem.Controllers.Validations;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using Utilites.Exstensions;

namespace RegistrationSystemTests.RegistrationSystem
{
    [SupportedOSPlatform("windows")]
    public class ValidationsTests
    {
        [Theory]
        [InlineData("povilas.velicka@cramo.com")]
        [InlineData("mail123@cramo.com")]
        [InlineData("gmail@gmail.lt")]
        public void AllowedEmailAttribut_WhenCorrectEmail_ReturnTrue(string email)
        {
            var attribut = new AllowedEmailsAttribute();

            Assert.True(attribut.IsValid(email));
        }

        [Theory]
        [InlineData("povilas.velicka@cramo .com")]
        [InlineData("sgssfgsfg. @cramo.com")]
        [InlineData("@cramo.com")]
        [InlineData("sgssfgsfgsfgsfgsdfgsfg@")]
        public void AllowedEmailAttribut_WhenEmailIncourrect_ReturnFalse(string email)
        {
            var attribut = new AllowedEmailsAttribute();

            Assert.False(attribut.IsValid(email));
        }

        [Theory]
        [InlineData("Pa$$w0rd")]
        [InlineData("Passw0rd")]
        [InlineData("Password123")]
        public void AllowedPasswordAttribute_WhenPasswordValid_ReturnTrue(string password)
        {
            var validator = new AllowedPasswordsAttribute();

            Assert.True(validator.IsValid(password));
        }

        [Theory]
        [InlineData("pa$$word")]
        [InlineData("passw0rd")]
        [InlineData("Password")]
        [InlineData("password")]
        [InlineData("Pa123")]
        [InlineData("PASSWORD123")]
        [InlineData(null)]
        public void AllowedPasswordAttribute_WhenPasswordNotValid_ReturnFalse(object password)
        {
            var validator = new AllowedPasswordsAttribute();

            Assert.False(validator.IsValid(password));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void AllowedStringInputAttribute_WhenStringEmptyOrNull_ReturnFalse(object stringInput)
        {
            var attribute = new AllowedInputStringAttribute();

            Assert.False(attribute.IsValid(stringInput));
        }

        [Theory]
        [InlineData("not empty string")]
        public void AllowedStringInputAttribute_WhenStringNotEmpty_ReturnTrue(object stringInput)
        {
            var attribute = new AllowedInputStringAttribute();

            Assert.True(attribute.IsValid(stringInput));
        }

        [Fact]
        public void AllowedImageAttribute_WhenImageFormatAndSizeCorrect_RetrunTrue()
        {
            var iFormImage = new FormFileTest();

            var attribute = new AllowedProfilePicturesAttribute();

            Assert.True(attribute.IsValid(iFormImage));
        }

        [Theory]
        [InlineData("notvalidtype.jpegg")]
        [InlineData("notvalidtype")]
        public void AllowedImageAttribute_WhenImageFormatInCorrect_RetrunFalse(string fileName)
        {
            var iFormImage = new FormFileTest();
            iFormImage.FileName = fileName;
            var attribute = new AllowedProfilePicturesAttribute();

            Assert.False(attribute.IsValid(iFormImage));
        }

        [Fact]
        public void AllowedImageAttribute_WhenSizeInorrect_RetrunFalse()
        {
            var iFormImage = new FormFileTest();
            iFormImage.Length++;
            var attribute = new AllowedProfilePicturesAttribute();

            Assert.False(attribute.IsValid(iFormImage));
        }

    }
}