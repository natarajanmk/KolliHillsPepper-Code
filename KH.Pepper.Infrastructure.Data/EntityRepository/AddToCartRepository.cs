using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class AddToCartRepository : BaseRepository<AddToCart>, IAddToCartRepository
    {
        public AddToCartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
