using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features
{
    public class CreateProductReview : IRequest<bool>
    {

        [Required]
        public ProductReviewDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateProductReview, bool>
        {

            private readonly IMapper _mapper;
            private readonly IProductReviewRepository _unitOfWork;

            public Handler(IProductReviewRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateProductReview request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.ProductReview>(request.commandDto);
                await _unitOfWork.AddAsync(dbEntity);

               // await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ProductReviewDto, KH.Pepper.Core.Domain.ProductReview>();
            }
        }
    }
}
