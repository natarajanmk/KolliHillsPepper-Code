using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("ProductPrice", Schema = "dbo")]
    public class ProductPrice : BaseEntity
    {

        [Required]        
        public int ProductId { get; set; }

        [Required]        
        public int ProductQtyId { get; set; }

        [Required]         
        public decimal? Price { get; set; }
        
        public Boolean? IsOfferAvailable { get; set; }

        public decimal? OfferAmount { get; set; }

        public String OfferDetails { get; set; }

        public Boolean? IsDisplayOnProduct { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ProductQtyId")]
        public ProductQuantity ProductQuantity { get; set; }
    }
}
