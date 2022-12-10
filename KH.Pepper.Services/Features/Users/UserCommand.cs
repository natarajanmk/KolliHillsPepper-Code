using AutoMapper;
using FluentValidation;
using KH.Pepper.Core.AppServices.Common;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.Features.Command
{
    public class UserCommand
    {
        public class Request : ICommand<bool>
        {
            [Required]
            public UserDto commandDto { get; set; }
        }

        public class AddToCartCommandRequestValidator : AbstractValidator<Request>
        {
            private readonly IUserRepository _repository;

            public AddToCartCommandRequestValidator(IUserRepository repository) : base()
            {
                _repository = repository;

                RuleFor(x => x.commandDto.PhoneNumber).NotEmpty().WithMessage(Keys.Validation.Required);

                CascadeMode = CascadeMode.Stop;
            }
        }

        public class Handler : IRequestHandler<CreateUser, bool>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                //var dbEntity = _mapper.Map<KH.Pepper.Core.Domain.User>(request.commandDto);
                var dbEntity = Mapper.Map<UserDto, User>(request.commandDto);

                var existingDbEntity = _userRepository.Any(x => x.EmailAddress == request.commandDto.EmailAddress ||
                                                                            x.PhoneNumber == request.commandDto.PhoneNumber);
                if (!existingDbEntity)
                {
                    await _userRepository.AddAsync(dbEntity);
                }
                else
                {
                    await _userRepository.UpdateAsync(dbEntity);
                }

                // await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }

        protected static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserDto, User>();
        }).CreateMapper();

        
    }

}
