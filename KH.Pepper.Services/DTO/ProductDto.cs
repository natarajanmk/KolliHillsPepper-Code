using AutoMapper;
using KH.Pepper.Core.Domain;


namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(Product), ReverseMap = true)]
    public class ProductDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public String Name { get; set; }

        public String ImageName { get; set; }

        public String ImagePath { get; set; }

        public String Description { get; set; }

        public String Benefits { get; set; }

        public String Tips { get; set; }

        public Boolean? StackAvailable { get; set; }

        public Boolean? IsActive { get; set; }

        public List<ProductPrice> ProductPrice { get; set; }
    }
}
