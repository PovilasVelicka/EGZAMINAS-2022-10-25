using RegistrationSystem.Entities.Models.AccountProperties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models
{
    [Table("UserInfos", Schema = "RegistrationSystem")]
    public class UserInfo
    {
        [Key]
        public Guid Id { get; set; }

        public FirstName FirstName { get; set; } = new FirstName( );

        public LastName LastName { get; set; } = new LastName( );

        public PersonalCode PersonalCode { get; set; } = new PersonalCode( );

        public Phone Phone { get; set; } = new Phone( );

        public Email Email { get; set; } = new Email( );

        public byte[ ] ProfilePicture { get; set; } = null!;


        [ForeignKey("Id")]
        [InverseProperty("UserInfo")]
        public virtual Account Account { get; set; } = null!;

        public virtual int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;
    }
}
