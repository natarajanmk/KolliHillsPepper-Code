using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.Infra.DataBase.UnitOfRepository
{
    public interface IUnitOfRepository  
    {
        
        IContactUsRepository ContactUsRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductPriceRepository ProductPriceRepository { get; }
        IProductQuantityRepository ProductQuantityRepository { get; }
        IUserRepository UserRepository { get; }
        IAddToCartRepository AddToCartRepository { get; }
        IProductReviewRepository ProductReviewRepository { get; }
        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
        IProductOrderDetailsRepository ProductOrderDetailsRepository { get; }


    }
}
