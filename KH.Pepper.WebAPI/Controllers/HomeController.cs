using KH.Pepper.Core.AppServices.Dto;
using KH.Pepper.Core.AppServices.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KH.Pepper.Web.ApiExtensions;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [ApiKeyAuthenticationFilter]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddContactUs", Name = "AddContactUs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CreateContactUs([FromBody][Required] ContactUsDto contactusDto)
        {
            var command = new CreateContactUs
            {
                commandDto = contactusDto
            };
            return await _mediator.Send(command);
        }

    }
}
