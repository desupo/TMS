using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TMS.host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private ISender _Mediator = null!;

    protected ISender _mediator => _Mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
