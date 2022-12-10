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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IProductRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<ProductDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductDto> GetProductById([FromRoute] int Id)
        {
            var dbEntity = _unitOfWork.GetById(x => x.Id == Id);
            var results = _mapper.Map<ProductDto>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpPut]
        [Route("Add", Name = "AddProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CreateProduct([FromBody][Required] ProductDto productDto)
        {
            var command = new CreateProduct
            {
                commandDto = productDto
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { Id = command.commandDto.Id }, result);
        }

        [HttpDelete]
        [Route("{Id}", Name = "ProductDelete")]
        public async Task<ActionResult<bool>> ProductDelete([FromRoute][Required] int Id)
        {
            var command = new DeleteCommand
            {
                id = Id,
                EntityModel = "Product"
            };
            return await _mediator.Send(command);
        }
    }
}
