using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.AppServices.Features;
using KH.Pepper.Core.Domain;
using KH.Pepper.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KH.Pepper.Web.ApiExtensions;
using System.ComponentModel.DataAnnotations;


namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [ApiKeyAuthenticationFilter]
    [Route("api/[controller]")]
    public class ProductReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductReviewRepository _unitOfWork;
        private readonly IMapper _mapper;

        public ProductReviewController(IMediator mediator, IProductReviewRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllProductReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductReviewDto>> GetAllProductReview()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<ProductReviewDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }
 

        [HttpPut]
        [Route("Add", Name = "AddProductReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CreateProductReview([FromBody][Required] ProductReviewDto reviewDto)
        {
            var command = new CreateProductReview
            {
                commandDto = reviewDto
            };
            return await _mediator.Send(command);
        }
    }
}
