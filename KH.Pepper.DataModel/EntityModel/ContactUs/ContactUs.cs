using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("ContactUs", Schema = "dbo")]
    public class ContactUs : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Descriptions { get; set; }
    }
}
