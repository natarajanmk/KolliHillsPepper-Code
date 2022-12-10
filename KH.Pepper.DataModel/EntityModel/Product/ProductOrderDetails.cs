using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    /* this is only for report */
    [Table("ProductOrderDetails", Schema = "dbo")]
    public class ProductOrderDetails : BaseEntity
    {

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal? Total { get; set; }

        public string OrderStatus { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
