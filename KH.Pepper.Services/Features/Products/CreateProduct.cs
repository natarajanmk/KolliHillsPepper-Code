using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features
{
    public class CreateProduct : IRequest<bool>
    {
        [Required]
        public ProductDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateProduct, bool>
        {

            private readonly IMapper _mapper;
            private readonly IProductRepository _unitOfWork;

            public Handler(IProductRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateProduct request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.Product>(request.commandDto);

                var existingDbEntity = _unitOfWork.GetById(x => x.Id == request.commandDto.Id);
                if (existingDbEntity is null)
                {
                    await _unitOfWork.AddAsync(dbEntity);
                }
                else
                {
                    await _unitOfWork.UpdateAsync(existingDbEntity);
                }
               // await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ProductDto, KH.Pepper.Core.Domain.Product>();
            }
        }
    }
}
