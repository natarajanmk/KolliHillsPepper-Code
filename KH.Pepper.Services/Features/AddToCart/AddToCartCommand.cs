using AutoMapper;
using FluentValidation;
using KH.Pepper.Core.AppServices.Common;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features.Command
{
    public class AddToCartCommand
    {
        public class Request : ICommand<AddToCart>
        {
            [Required]
            public AddToCartDto commandDto { get; set; }
        }

        public class AddToCartCommandRequestValidator : AbstractValidator<Request>
        {
            private readonly IAddToCartRepository _addToCartRepository;

            public AddToCartCommandRequestValidator(IAddToCartRepository addToCartRepository) : base()
            {
                _addToCartRepository = addToCartRepository;

                RuleFor(x => x.commandDto.UserId).NotEmpty().WithMessage(Keys.Validation.Required);
                RuleFor(x => x.commandDto.ProductId).NotEmpty().WithMessage(Keys.Validation.Required);

                CascadeMode = CascadeMode.Stop;
            }
        }

        public class Handler : IRequestHandler<Request, AddToCart>
        {
            private readonly IAddToCartRepository _addToCartRepository;
            private readonly IMapper _mapper;

            public Handler(IAddToCartRepository addToCartRepository, IMapper mapper)
            {
                _addToCartRepository = addToCartRepository;
                _mapper = mapper;
            }

            public async Task<AddToCart> Handle(Request request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                //var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.AddToCart>(request.commandDto);
                var dbEntity = Mapper.Map<AddToCartDto, AddToCart>(request.commandDto);

                var existingDbEntity = _addToCartRepository.GetById(x => x.Id == request.commandDto.Id);

                if (existingDbEntity is null)
                {
                    await _addToCartRepository.AddAsync(dbEntity);
                }
                else
                {
                    await _addToCartRepository.UpdateAsync(existingDbEntity);
                }
                //await _unitOfWork.SaveChangesAsync();

                return await Task.FromResult(dbEntity);

            }
        }

        protected static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AddToCartDto, AddToCart>();
        }).CreateMapper();

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<AddToCartDto, AddToCart>();
            }
        }
    }
}
