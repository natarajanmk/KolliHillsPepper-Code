using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("ProductQuantity", Schema = "dbo")]
    public class ProductQuantity : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public int Type { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public String Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String Description { get; set; }

        public Boolean? IsActive { get; set; }
         
    }
}
