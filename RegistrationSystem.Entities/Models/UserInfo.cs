using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models
{
    [Table("UserInfos",Schema = "RegistrationSystem")]
    public class UserInfo
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; } = null!;
        [StringLength(100)]
        public string LastName { get; set; } = null!;
        [StringLength(20)]
        public string PersonalCode { get; set; } = null!;
        [StringLength(100)]
        public string Phone { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        public byte[ ] Picture { get; set; } = null!;
        public int LivingAddressId { get; set; }

        public int AddressId { get; set; }
        public Address LivingAddress { get; set; } = null!;
    }
}
