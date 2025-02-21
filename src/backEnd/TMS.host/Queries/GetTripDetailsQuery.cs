using MediatR;
using TMS.application.DTOs;

namespace TMS.host.Queries;
public class GetTripDetailsQuery : IRequest<TripDetailsDto>
{
    public long TripId { get; set; }
}