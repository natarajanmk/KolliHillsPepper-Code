using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Infrastructure.DataBase;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features.AddToCarts
{
    public class CreateAddToCart : IRequest<bool>
    {
        [Required]
        public AddToCartDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateAddToCart, bool>
        {

            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateAddToCart request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.AddToCart>(request.commandDto);

                var existingDbEntity = _unitOfWork.AddToCartRepository.GetById(x => x.Id == request.commandDto.Id);

                if (existingDbEntity is null)
                {
                    await _unitOfWork.AddToCartRepository.AddAsync(dbEntity);
                }
                else
                {
                    await _unitOfWork.AddToCartRepository.UpdateAsync(existingDbEntity);
                }
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<AddToCartDto, KH.Pepper.Core.Domain.AddToCart>();
            }
        }
    }
}
