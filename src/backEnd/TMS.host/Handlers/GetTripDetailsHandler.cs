using MediatR;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.host.Queries;

namespace TMS.host.Handlers;

public class GetTripDetailsHandler : IRequestHandler<GetTripDetailsQuery, TripDetailsDto>
{
    private readonly ITripService _tripService;

    public GetTripDetailsHandler(ITripService tripService)
    {
        _tripService = tripService;
    }

    public async Task<TripDetailsDto> Handle(GetTripDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _tripService.GetTripDetailsAsync(request.TripId);
    }
}
