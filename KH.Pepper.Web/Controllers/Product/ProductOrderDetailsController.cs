using AutoMapper;
using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.Domain;
using KH.Pepper.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KH.Pepper.Web.ApiExtensions;



//this is only for view the user ordered details
namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [ApiKeyAuthenticationFilter]
    [Route("api/[controller]")]
    public class ProductOrderDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductOrderDetailsRepository _unitOfWork;
        private readonly IMapper _mapper;

        public ProductOrderDetailsController(IMediator mediator, IProductOrderDetailsRepository unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllProductOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductOrderDetailsDto>> GetAllProductOrderDetails()
        {
            var dbEntity = _unitOfWork.GetAll();
            var results = _mapper.Map<IEnumerable<ProductOrderDetailsDto>>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetProductOrderDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductOrderDetailsDto> GetProductOrderDetailsById([FromRoute] int Id)
        {
            var dbEntity = _unitOfWork.GetById(x => x.Id == Id);
            var results = _mapper.Map<ProductOrderDetailsDto>(dbEntity);
            if (results is null)
            {
                throw new ExceptionHandle("No records found.");
            }
            return Ok(results);
        }
         
    }
}
