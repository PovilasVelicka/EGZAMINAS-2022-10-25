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
        public byte[ ] PasswordHash { get; set; } = null!;
        [MaxLength(460)]
        public byte[ ] PasswordSalt { get; set; } = null!;
        [StringLength(20)]
        public UserRole Role { get; set; }

        [InverseProperty("Account")]
        public virtual UserInfo UserInfo { get; set; } = null!;
    }
}
