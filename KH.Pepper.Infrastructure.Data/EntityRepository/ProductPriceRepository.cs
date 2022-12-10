using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ProductPriceRepository : BaseRepository<ProductPrice>, IProductPriceRepository
    {
        public ProductPriceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
