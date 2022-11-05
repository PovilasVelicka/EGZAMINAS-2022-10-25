using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RegistrationSystem.Entities.Models
{
    [Table("Streets", Schema = "RegistrationSystem")]
    [Index("Name", Name = "UI_StreetName", IsUnique = true)]
    public class Street
    {
        public Street ( )
        {
            Addresses = new HashSet<Address>( );
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public virtual ICollection<Address> Addresses { get; set; }
    }
}
