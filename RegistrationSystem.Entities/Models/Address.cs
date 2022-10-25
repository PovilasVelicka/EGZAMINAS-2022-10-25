using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Entities.Models
{
    [Table("Addresses", Schema = "RegistrationSystem")]
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string AppartmentNumber { get; set; }=null!;

        public ICollection<UserInfo> UserInfos { get; set; } = null!;
    }
}
