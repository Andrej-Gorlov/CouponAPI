using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouponAPI.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();
    }
}
