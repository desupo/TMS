using MediatR;
using TMS.application.Interfaces;

namespace TMS.host.Commands;

public class ProcessEquipmentEventsCommand : IRequest<Unit>
{
    public IFormFile File { get; set; }
}


