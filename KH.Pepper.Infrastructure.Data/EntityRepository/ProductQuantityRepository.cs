using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ProductQuantityRepository : BaseRepository<ProductQuantity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
