using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    private ILogger _logger;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    internal Guid UserId
    {
        get
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier );
            if (userIdClaim == null)
            {
                throw new InvalidOperationException("UserId claim is missing.");
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId format.");
            }

            return userId;
        }
    }
}