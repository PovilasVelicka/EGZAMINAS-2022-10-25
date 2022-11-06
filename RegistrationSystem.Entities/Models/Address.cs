using RegistrationSystem.Entities.Models.AccountProperties;
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
     
        public City City { get; set; } = new City();
    
        public Street Street { get; set; } = new Street();
 
        public HouseNumber HouseNumber { get; set; } = null!;
      
        public AppartmentNumber AppartmentNumber { get; set; } = null!;

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
