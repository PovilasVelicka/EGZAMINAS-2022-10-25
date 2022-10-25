﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models
{
    [Table("UserInfos", Schema = "RegistrationSystem")]
    public class UserInfo
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; } = null!;
        [StringLength(150)]
        public string LastName { get; set; } = null!;
        [StringLength(20)]
        public string PersonalCode { get; set; } = null!;
        [StringLength(150)]
        public string Phone { get; set; } = null!;
        [StringLength(150)]
        public string Email { get; set; } = null!;

        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
    }
}
