
using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.AppServices.Features;
using KH.Pepper.Core.Domain;
using KH.Pepper.Services;
using KH.Pepper.Web.ApiExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [ApiKeyAuthenticationFilter]
    [Route("api/[controller]")]
    public class AddToCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAddToCartRepository _unitOfWork;
        private readonly IMapper _mapper;

        public AddToCartController(IMediator mediator, IAddToCartRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllAddToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AddToCartDto>> GetAllAddToCart()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<AddToCartDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }
 

        //[HttpPut]
        //[Route("Add", Name = "AddAddToCart")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<bool>> CreateAddToCart([FromBody][Required] AddToCartDto cartDto )
        //{
        //    var command = new CreateAddToCart
        //    {
        //        commandDto = cartDto
        //    };
        //    return await _mediator.Send(command);
        //}


        [HttpDelete]
        [Route("{Id}", Name = "AddToCartDelete")]
        public async Task<ActionResult<bool>> AddToCartDelete([FromRoute][Required] int Id)
        {
            var command = new DeleteCommand
            {
                id = Id,
                EntityModel = "AddToCart"
            };
            return await _mediator.Send(command);
        }
    }
}
