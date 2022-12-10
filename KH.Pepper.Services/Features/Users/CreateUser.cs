using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features
{
    public class CreateUser : IRequest<bool>
    {
        [Required]
        public UserDto commandDto { get; set; }

        public class Handler : IRequestHandler<CreateUser, bool>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _unitOfWork;

            public Handler(IUserRepository unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.User>(request.commandDto);

                var existingDbEntity = _unitOfWork.Any(x => x.EmailAddress == request.commandDto.EmailAddress ||
                                                                            x.PhoneNumber == request.commandDto.PhoneNumber);
                if (!existingDbEntity)
                {
                    await _unitOfWork.AddAsync(dbEntity);
                }
                else
                {
                    await _unitOfWork.UpdateAsync(dbEntity);
                }

               // await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<UserDto, KH.Pepper.Core.Domain.User>();
            }
        }
    }
}
