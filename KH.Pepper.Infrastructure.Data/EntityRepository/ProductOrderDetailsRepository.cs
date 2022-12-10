using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ProductOrderDetailsRepository : BaseRepository<ProductOrderDetails>, IProductOrderDetailsRepository
    {
        public ProductOrderDetailsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
