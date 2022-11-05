using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RegistrationSystem.Entities.Models
{
    [Table("Cities", Schema = "RegistrationSystem")]
    [Index("Name", Name = "UI_CityName", IsUnique = true)]
    public class City
    {
        public City ( )
        {
            Addresses = new HashSet<Address>( );
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
