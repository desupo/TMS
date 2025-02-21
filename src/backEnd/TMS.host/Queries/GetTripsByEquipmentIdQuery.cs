using MediatR;
using TMS.application.DTOs;

namespace TMS.host.Queries;

public class GetTripsByEquipmentIdQuery : IRequest<List<TripDetailsDto>>
{
    public string EquipmentId { get; set; }
}

