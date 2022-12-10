using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ProductReviewRepository : BaseRepository<ProductReview>, IProductReviewRepository
    {
        public ProductReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
