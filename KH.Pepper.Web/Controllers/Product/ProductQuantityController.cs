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
    public class ProductQuantityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductQuantityRepository _unitOfWork;
        private readonly IMapper _mapper;

        public ProductQuantityController(IMediator mediator, IProductQuantityRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllProductQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductQuantityDto>> GetAllProductQuantity()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<ProductQuantityDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetProductQuantityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductQuantityDto> GetProductQuantityById([FromRoute] int Id)
        {
            var dbEntity = _unitOfWork.GetById(x => x.Id == Id);
            var results = _mapper.Map<ProductQuantityDto>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpPut]
        [Route("Add", Name = "AddProductQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> AddProductQuantity([FromBody][Required] ProductQuantityDto qtyDto)
        {
            var command = new CreateProductQuantity
            {
                commandDto = qtyDto
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductQuantityById), new { Id = command.commandDto.Id }, result);
        }

        //[HttpDelete]
        //[Route("{Id}", Name = "ProductQuantityDelete")]
        //public async Task<ActionResult<bool>> ProductQuantityDelete([FromRoute][Required] int Id)
        //{
        //    var command = new DeleteCommand
        //    {
        //        id = Id,
        //        EntityModel = "ProductQuantity"
        //    };
        //    return await _mediator.Send(command);
        //}
    }
}
