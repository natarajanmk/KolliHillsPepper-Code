
using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using KH.Pepper.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KH.Pepper.Web.ApiExtensions;
using System.ComponentModel.DataAnnotations;
using KH.Pepper.Core.AppServices.Features;

namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [ApiKeyAuthenticationFilter]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IUserRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<UserDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserDto> GetUserById([FromRoute] int Id)
        {
            var dbEntity = _unitOfWork.GetById(x => x.Id == Id);
            var results = _mapper.Map<UserDto>(dbEntity);
            if(results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpPut]
        [Route("Add", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AddUser([FromBody][Required] UserDto userDto)
        {
            var command = new CreateUser
            {
                commandDto = userDto
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { Id = command.commandDto.Id }, result);
        }

        //[HttpDelete]
        //[Route("{Id}", Name = "UserDelete")]
        //public async Task<ActionResult<bool>> UserDelete([FromRoute][Required] int Id)
        //{
        //    var command = new DeleteCommand
        //    {
        //        id = Id,
        //        EntityModel = "User"
        //    };
        //    return await _mediator.Send(command);
        //}
    }

}
