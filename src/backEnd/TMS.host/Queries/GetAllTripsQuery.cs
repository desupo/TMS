using MediatR;
using TMS.application.DTOs;

namespace TMS.host.Queries;

public class GetAllTripsQuery : IRequest<List<TripDetailsDto>>
{
}

