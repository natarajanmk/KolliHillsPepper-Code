using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase;

namespace KH.Pepper.Core.Infra.DataBase.UnitOfRepository
{
    public class UnitOfRepository : IUnitOfRepository
    {
        private AppDbContext _context;

        public UnitOfRepository(AppDbContext context)
        {
            this._context = context;
        }
        
        public IContactUsRepository ContactUsRepository =>  new ContactUsRepository(_context);

        public IProductRepository ProductRepository =>  new ProductRepository(_context);

        public IProductPriceRepository ProductPriceRepository =>  new ProductPriceRepository(_context);

        public IProductQuantityRepository ProductQuantityRepository =>  new ProductQuantityRepository(_context);

        public IUserRepository UserRepository =>  new UserRepository(_context);

        public IAddToCartRepository AddToCartRepository => new AddToCartRepository(_context);

        public IProductReviewRepository ProductReviewRepository => new ProductReviewRepository(_context);
        public IUserRefreshTokenRepository UserRefreshTokenRepository => new UserRefreshTokenRepository(_context);

        public IProductOrderDetailsRepository ProductOrderDetailsRepository => new ProductOrderDetailsRepository(_context);

        
    }
}
