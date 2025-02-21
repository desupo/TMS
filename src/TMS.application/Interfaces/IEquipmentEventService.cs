using Microsoft.AspNetCore.Http;
using TMS.domain.Entities;

namespace TMS.application.Interfaces;

public interface IEquipmentEventService
{
    Task ProcessEventsAsync(Stream csvStream);
    Task ProcessEventsAsync(IEnumerable<Event> events);
    Task<IEnumerable<Trip>> GetTripsAsync();
    Task<IEnumerable<EquipmentEvent>> GetEventsByTripIdAsync(long tripId);
    Task<IEnumerable<Event>> ParseCsvAsync(IFormFile file);
}
