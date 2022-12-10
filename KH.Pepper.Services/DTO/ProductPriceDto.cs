using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(ProductPrice), ReverseMap = true)]
    public class ProductPriceDto
    {

        public int Id { get; set; }
        public int ProductId { get; set; }

        
        public int ProductQtyId { get; set; }

        
        public decimal? Price { get; set; }

        public Boolean? IsOfferAvailable { get; set; }

        public decimal? OfferAmount { get; set; }

        public String OfferDetails { get; set; }

        public Boolean? IsDisplayOnProduct { get; set; }
 

    }
}
