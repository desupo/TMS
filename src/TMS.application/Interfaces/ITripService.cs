using TMS.application.DTOs;
using TMS.domain.Entities;

namespace TMS.application.Interfaces;

public interface ITripService
{
    /// <summary>
    /// Get trip details by trip id
    /// </summary>
    /// <param name="tripId"></param>
    /// <returns></returns>
    Task<TripDetailsDto> GetTripDetailsAsync(long tripId);
    /// <summary>
    /// Get all trips
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Trip>> GetTripsAsync();
    /// <summary>
    /// Get events by trip id
    /// </summary>
    /// <param name="tripId"></param>
    /// <returns></returns>
    Task<IEnumerable<Event>> GetEventsByTripIdAsync(long tripId);
    /// <summary>
    /// Get trips by equipment id
    /// </summary>
    /// <param name="equipmentId"></param>
    /// <returns></returns>
    Task<List<TripDetailsDto>> GetTripsByEquipmentIdAsync(string equipmentId);
    /// <summary>
    /// Get all trips
    /// </summary>
    /// <returns></returns>
    Task<List<TripDetailsDto>> GetAllTripsAsync();
}