using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.host.Commands;

namespace TMS.host.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController(IMediator mediator) : ControllerBase
{
   private readonly IMediator _mediator = mediator;
    [HttpPost("upload")]
    public async Task<IActionResult> UploadEventsAsync([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty or not provided.");
        }

        var command = new ProcessEquipmentEventsCommand { File = file };
        await _mediator.Send(command);

        return Ok();
    }
}
