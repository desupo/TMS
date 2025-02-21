using MediatR;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.host.Queries;

namespace TMS.host.Handlers;

public class GetTripsByEquipmentIdHandler : IRequestHandler<GetTripsByEquipmentIdQuery, List<TripDetailsDto>>
{
    private readonly ITripService _tripService;

    public GetTripsByEquipmentIdHandler(ITripService tripService)
    {
        _tripService = tripService;
    }

    public async Task<List<TripDetailsDto>> Handle(GetTripsByEquipmentIdQuery request, CancellationToken cancellationToken)
    {
        return await _tripService.GetTripsByEquipmentIdAsync(request.EquipmentId);
    }
}
