using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("Product", Schema = "dbo")]
    public class Product : BaseEntity
    {
        [Column(TypeName = "nvarchar(max)")]
        public String Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String NameInTamil { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String ImageName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String ImagePath { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String Benefits { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public String Tips { get; set; }

        public Boolean? StackAvailable { get; set; }

        public Boolean? IsActive { get; set; }

        public List<ProductPrice> ProductPrice { get; set; }
    }
}
