using MediatR;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.host.Queries;

namespace TMS.host.Handlers;

public class GetAllTripsHandler : IRequestHandler<GetAllTripsQuery, List<TripDetailsDto>>
{
    private readonly ITripService _tripService;

    public GetAllTripsHandler(ITripService tripService)
    {
        _tripService = tripService;
    }

    public async Task<List<TripDetailsDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        return await _tripService.GetAllTripsAsync();
    }
}
