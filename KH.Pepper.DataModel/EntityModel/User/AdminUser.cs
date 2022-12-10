using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("AdminUser", Schema = "dbo")]
    public class AdminUser  
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
         
        public Boolean? IsActive { get; set; }
    }
}
