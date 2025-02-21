using MediatR;
using TMS.application.Interfaces;
using TMS.host.Commands;

namespace TMS.host.Handlers;

public class ProcessEventsHandler(IEquipmentEventService service) : IRequestHandler<ProcessEquipmentEventsCommand, Unit>
{
    private readonly IEquipmentEventService _service = service;

    public async Task<Unit> Handle(ProcessEquipmentEventsCommand request, CancellationToken cancellationToken)
    {
        var events = await _service.ParseCsvAsync(request.File);
        await _service.ProcessEventsAsync(events);
        return Unit.Value;
    }
}
