using Microsoft.AspNetCore.Http;
using TMS.domain.Entities;

namespace TMS.application.Interfaces;

public interface IEquipmentEventService
{
    Task ProcessEventsAsync(IEnumerable<Event> events);
    Task ProcessEvents(IEnumerable<Event> events);
    Task<IEnumerable<Event>> ParseCsvAsync(IFormFile file);
}
