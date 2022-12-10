using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("ProductReview", Schema = "dbo")]
    public class ProductReview : BaseEntity
    {       
        [Required]
        public int? UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
         
        [Column(TypeName = "nvarchar(max)")]
        public String Comments { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public String Rating { get; set; }
    }
}
