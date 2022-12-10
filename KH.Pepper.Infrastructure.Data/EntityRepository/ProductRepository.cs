using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
