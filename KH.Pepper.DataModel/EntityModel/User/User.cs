using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("User", Schema = "dbo")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]    
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get;  set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string OneTimePassword { get; set; }

        public Boolean? IsActive { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Location { get; set; }

        public string PinCode { get; set; }

        public List<UserRefreshToken> UserRefreshTokens { get; set; }

    }
}
