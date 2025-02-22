using Microsoft.AspNetCore.Http;
using TMS.domain.Entities;

namespace TMS.application.Interfaces;

public interface IEquipmentEventService
{
    Task ProcessEventsAsync(List<Event> events);
    Task ProcessEvents(List<Event> events);
    Task<List<Event>> ParseCsvAsync(IFormFile file);
}
