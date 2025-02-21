using Microsoft.AspNetCore.Mvc;
using TMS.host.Queries;

namespace TMS.host.Controllers;

public class TripsController : BaseApiController
{
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetTrip(long id)
    {
        var query = new GetTripDetailsQuery { TripId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("equipment/{equipmentId}")]
    public async Task<IActionResult> GetTripsByEquipmentId(string equipmentId)
    {
        var query = new GetTripsByEquipmentIdQuery { EquipmentId = equipmentId };
        var result = await _mediator.Send(query);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrips()
    {
        var query = new GetAllTripsQuery();
        var result = await _mediator.Send(query);

        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }
}
