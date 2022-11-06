using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RegistrationSystem.Entities.Models.AccountProperties
{
    public class AddressProperties
    {
        public AddressProperties ( )
        {
            Addresses = new HashSet<Address>( );
        }

        public int Id { get; set; }

        [StringLength(256)]
        public virtual string Value { get; set; } = null!;

        public virtual ICollection<Address> Addresses { get; set; }
    }

    [Table("Cities", Schema = "AddressProperties")]
    [Index("Value", Name = "UI_AddressProperties_City", IsUnique = true)]
    public class City : AddressProperties { }

    [Table("Streets", Schema = "AddressProperties")]
    [Index("Value", Name = "UI_AddressProperties_Street", IsUnique = true)]
    public class Street : AddressProperties { }

    [Table("HouseNumbers", Schema = "AddressProperties")]
    [Index("Value", Name = "UI_AddressProperties_HouseNumber", IsUnique = true)]
    public class HouseNumber : AddressProperties { }

    [Table("AppartmentNumbers", Schema = "AddressProperties")]
    [Index("Value", Name = "UI_AddressProperties_AppartmenNumber", IsUnique = true)]
    public class AppartmentNumber : AddressProperties { }
}
