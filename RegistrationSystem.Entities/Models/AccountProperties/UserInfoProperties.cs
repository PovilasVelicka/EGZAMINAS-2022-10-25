using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Entities.Models.AccountProperties
{
    public class UserInfoProperties
    {
        public UserInfoProperties ( )
        {
            UserInfos=new HashSet<UserInfo>();
        } 

        public int Id { get; set; }

        [StringLength(256)]
        public virtual string Value { get; set; } = null!;   

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }

    [Table("FirstNames", Schema = "UserInfoProperties")]
    [Index("Value", Name = "UI_UserInfoProperties_FirstName", IsUnique = true)]
    public class FirstName : UserInfoProperties { }

    [Table("LastNames", Schema = "UserInfoProperties")]
    [Index("Value", Name = "UI_UserInfoProperties_LastName", IsUnique = true)]
    public class LastName : UserInfoProperties { }

    [Table("PersonalCodes", Schema = "UserInfoProperties")]
    [Index("Value", Name = "UI_UserInfoProperties_PersonalCode", IsUnique = true)]
    public class PersonalCode : UserInfoProperties { }

    [Table("Phones", Schema = "UserInfoProperties")]
    [Index("Value", Name = "UI_UserInfoProperties_Phone", IsUnique = true)]
    public class Phone : UserInfoProperties { }

    [Table("Emails", Schema = "UserInfoProperties")]
    [Index("Value", Name = "UI_UserInfoProperties_Email", IsUnique = true)]
    public class Email : UserInfoProperties { }
}
