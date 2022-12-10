using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features
{
    public class CreateProductQuantity : IRequest<bool>
    {
        [Required]
        public ProductQuantityDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateProductQuantity, bool>
        {

            private readonly IMapper _mapper;
            private readonly IProductQuantityRepository _unitOfWork;

            public Handler(IProductQuantityRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateProductQuantity request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.ProductQuantity>(request.commandDto);

                var existingDbEntity = _unitOfWork.GetById(x => x.Id == request.commandDto.Id);
                if (existingDbEntity is null)
                {
                    await _unitOfWork.AddAsync(dbEntity);
                }
                else
                {
                    await _unitOfWork.UpdateAsync(existingDbEntity);
                }

                //await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ProductQuantityDto, KH.Pepper.Core.Domain.ProductQuantity>();
            }
        }
    }
}
