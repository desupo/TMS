using TMS.application.DTOs;
using TMS.domain.Entities;

namespace TMS.application.Interfaces;

public interface ITripService
{
    Task<TripDetailsDto> GetTripDetailsAsync(long tripId);
    Task<IEnumerable<Trip>> GetTripsAsync();
    Task<IEnumerable<Event>> GetEventsByTripIdAsync(long tripId);
    Task<List<TripDetailsDto>> GetTripsByEquipmentIdAsync(string equipmentId);
    Task<List<TripDetailsDto>> GetAllTripsAsync();
}