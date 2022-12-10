using MediatR;
using System.ComponentModel.DataAnnotations;
using KH.Pepper.Core.Infra.DataBase.UnitOfRepository;

namespace KH.Pepper.Core.AppServices.Features
{
    public class DeleteCommand : IRequest<bool>
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string EntityModel { get; set; }

        public class Handler : IRequestHandler<DeleteCommand, bool>
        {

            private readonly IUnitOfRepository _unitOfRepository;

            public Handler(IUnitOfRepository unitOfRepository)
            {
                _unitOfRepository = unitOfRepository;
            }
            public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                bool IsRecordsAvailable = false;

                switch (request.EntityModel)
                {
                    case "Product":
                        if (_unitOfRepository.ProductRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.ProductRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.ProductRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "ProductOrderDetails":
                        if (_unitOfRepository.ProductOrderDetailsRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.ProductOrderDetailsRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.ProductOrderDetailsRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "ProductPrice":
                        if (_unitOfRepository.ProductPriceRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.ProductPriceRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.ProductPriceRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "ProductQuantity":
                        if (_unitOfRepository.ProductQuantityRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.ProductQuantityRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.ProductQuantityRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "ProductReview":
                        if (_unitOfRepository.ProductReviewRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.ProductReviewRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.ProductReviewRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "AddToCart":
                        if (_unitOfRepository.AddToCartRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.AddToCartRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.AddToCartRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "UserRefreshToken":
                        if (_unitOfRepository.UserRefreshTokenRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.UserRefreshTokenRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.UserRefreshTokenRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;
                    case "User":
                        if (_unitOfRepository.UserRepository.Any(x => x.Id == request.id))
                        {
                            var dbEntity = _unitOfRepository.UserRepository.GetById(x => x.Id == request.id);
                            await _unitOfRepository.UserRepository.DeleteAsync(dbEntity);
                            IsRecordsAvailable = true;
                        }
                        break;

                }

                if (!IsRecordsAvailable)
                {
                    throw new ApplicationException("Records not found.");                     
                }
                else
                {
                    //await _unitOfRepository.SaveChangesAsync(cancellationToken);
                }
                return true;
            }

        }
    }
}
