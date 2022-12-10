using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features
{
    public class CreateContactUs : IRequest<bool>
    {
        [Required]
        public ContactUsDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateContactUs, bool>
        {

            private readonly IMapper _mapper;
            private readonly IContactUsRepository _unitOfWork;

            public Handler(IContactUsRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateContactUs request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.ContactUs>(request.commandDto);

                await _unitOfWork.AddAsync(dbEntity);

               // await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ContactUsDto, KH.Pepper.Core.Domain.ContactUs>();
            }
        }
    }
}
