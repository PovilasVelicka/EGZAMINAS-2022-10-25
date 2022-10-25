using RegistrationSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models
{
    [Table("Accounts", Schema = "RegistrationSystem")]
    public class Account
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string LoginName { get; set; } = null!;
        [MaxLength(460)]
        public byte[ ] Password { get; set; } = null!;
        [MaxLength(460)]
        public byte[ ] Salt { get; set; } = null!;
        public UserRole Role { get; set; }     
        public byte[] Photo { get; set; }=null!;



        public UserInfo? UserInfo { get; set; }
    }
}
