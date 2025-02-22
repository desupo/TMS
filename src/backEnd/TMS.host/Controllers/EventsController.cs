using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.application.DTOs;
using TMS.host.Commands;

namespace TMS.host.Controllers;

public class EventsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadEventsAsync([FromForm] UploadEventRequest request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest("File is empty or not provided.");
        }

        var command = new ProcessEquipmentEventsCommand { File = request.File };
        await _mediator.Send(command);

        return Ok();
    }

}
