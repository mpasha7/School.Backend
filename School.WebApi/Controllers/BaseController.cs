using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace School.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); // TODO: Может GetRequiredService() ?

        internal string UserGuid => !User.Identity?.IsAuthenticated ?? true
            ? string.Empty
            : User.FindFirst(ClaimTypes.NameIdentifier)!.Value; // TODO: Перевести на Guid (Почему в Identity применяется string, а не UUID?)
            //: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
