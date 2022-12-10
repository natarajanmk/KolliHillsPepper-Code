using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{
    [AutoMap(typeof(AddToCart), ReverseMap = true)]
    public class AddToCartDto
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }

        public int ProductId { get; set; }

        public decimal? Price { get; set; }

        public int Quantity { get; set; }

        public decimal? Total { get; set; }
        
    }
}
