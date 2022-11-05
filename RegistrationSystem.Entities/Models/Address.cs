using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models
{
    [Table("Addresses", Schema = "RegistrationSystem")]
    public class Address
    {
        public Address ( )
        {
            UserInfos = new HashSet<UserInfo>( );
        }

        public int Id { get; set; }
     
        public City City { get; set; } = null!;
    
        public Street Street { get; set; } = null!;
        [StringLength(10)]
        public string HouseNumber { get; set; } = null!;
        [StringLength(10)]
        public string AppartmentNumber { get; set; } = null!;

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
