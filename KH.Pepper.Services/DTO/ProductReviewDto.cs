using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(ProductReview), ReverseMap = true)]
    public class ProductReviewDto
    {

        public int Id { get; set; }
        public int? UserId { get; set; }
 
        public int ProductId { get; set; }
 
        public String Comments { get; set; }

         
        public String Rating { get; set; }
    }
}
