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
    public class ProductPriceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductPriceRepository _unitOfWork;
        private readonly IMapper _mapper;

        public ProductPriceController(IMediator mediator, IProductPriceRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllProductPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductPriceDto>> GetAllProductPrice()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<ProductPriceDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetProductPriceById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductPriceDto> GetProductPriceById([FromRoute] int Id)
        {
            var dbEntity = _unitOfWork.GetById(x => x.Id == Id);
            var results = _mapper.Map<ProductPriceDto>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpPut]
        [Route("Add", Name = "AddProductPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CreateProductPrice([FromBody][Required] ProductPriceDto priceDto)
        {
            var command = new CreateProductPrice
            {
                commandDto = priceDto
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductPriceById), new { Id = command.commandDto.Id }, result);
        }
       
        //[HttpDelete]
        //[Route("{Id}", Name = "ProductPriceDelete")]
        //public async Task<ActionResult<bool>> ProductPriceDelete([FromRoute][Required] int Id)
        //{
        //    var command = new DeleteCommand
        //    {
        //        id = Id,
        //        EntityModel = "ProductPrice"
        //    };
        //    return await _mediator.Send(command);
        //}
    }
}
