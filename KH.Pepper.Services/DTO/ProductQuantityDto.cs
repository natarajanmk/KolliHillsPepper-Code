using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(ProductQuantity), ReverseMap = true)]
    public class ProductQuantityDto
    {
        public int Id { get; set; }

        
        public int Type { get; set; }

        
        public String Name { get; set; }

       
        public String Description { get; set; }

        public Boolean? IsActive { get; set; }
    }
}
