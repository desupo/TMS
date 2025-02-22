using Microsoft.EntityFrameworkCore;
using TMS.infra.Persistence.Context;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.domain.Entities;
using Microsoft.Extensions.Logging;
namespace TMS.infra.Services;

public class TripService(dbContext context, ILogger<TripService> logger) : ITripService
{
    private readonly dbContext _context = context;
    private readonly ILogger<TripService> _logger = logger;
    //Inherited from ITripService
    public async Task<TripDetailsDto> GetTripDetailsAsync(long tripId)
    {
        try
        {
            var trip = await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .Include(t => t.Events).ThenInclude(x => x.City)
            .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null)
            {
                return null;
            }

            var tripDetails = new TripDetailsDto
            {
                TripId = trip.Id,
                EquipmentId = trip.EquipmentId,
                OriginCity = trip.Origin_City.Name,
                DestinationCity = trip.Destination_City.Name,
                StartDateUtc = trip.Start_Date.UtcDateTime,
                EndDateUtc = trip.End_Date.UtcDateTime,
                TotalHours = trip.TotalTripHours,
                //HasIssue = trip.HasIssue,
                //Completed = trip.Completed,
                Events = trip.Events.OrderBy(e => e.EventDate).Select(x => new RailcarEventModel
                {
                    EventCode = x.Code,
                    EventTimeUtc = x.EventDate.UtcDateTime,
                    City = x.City.Name,
                }).ToList()
            };
            return tripDetails;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting trip details");
            return new TripDetailsDto();
        }
    }
    //Inherited from ITripService
    public async Task<List<TripDetailsDto>> GetTripsByEquipmentIdAsync(string equipmentId)
    {
        try
        {
            var trips = await _context.Trips
           .Include(t => t.Origin_City)
           .Include(t => t.Destination_City)
           .Include(t => t.Events).ThenInclude(x => x.City)
           .Where(t => t.EquipmentId == equipmentId)
           .ToListAsync();

            return trips.Select(trip => new TripDetailsDto
            {
                TripId = trip.Id,
                EquipmentId = trip.EquipmentId,
                OriginCity = trip.Origin_City.Name,
                DestinationCity = trip.Destination_City.Name,
                StartDateUtc = trip.Start_Date.UtcDateTime,
                EndDateUtc = trip.End_Date.UtcDateTime,
                TotalHours = trip.TotalTripHours,
                //HasIssue = trip.HasIssue,
                //Completed = trip.Completed,
                Events = trip.Events.OrderBy(e => e.EventDate).Select(x => new RailcarEventModel
                {
                    EventCode = x.Code,
                    EventTimeUtc = x.EventDate.UtcDateTime,
                    City = x.City.Name,
                }).ToList()
            }).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting trips by equipment id");
            return [];
        }

    }
    //Inherited from ITripService
    public async Task<List<TripDetailsDto>> GetAllTripsAsync()
    {
        try
        {
            var trips = await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .Include(t => t.Events).ThenInclude(x => x.City)
            .ToListAsync();

            return trips.Select(trip => new TripDetailsDto
            {
                TripId = trip.Id,
                EquipmentId = trip.EquipmentId,
                OriginCity = trip.Origin_City.Name,
                DestinationCity = trip.Destination_City.Name,
                StartDateUtc = trip.Start_Date.UtcDateTime,
                EndDateUtc = trip.End_Date.UtcDateTime,
                TotalHours = trip.TotalTripHours,
                //HasIssue = trip.HasIssue,
                //Completed = trip.Completed,
                Events = trip.Events.OrderBy(e => e.EventDate).Select(x => new RailcarEventModel
                {
                    EventCode = x.Code,
                    EventTimeUtc = x.EventDate.UtcDateTime,
                    City = x.City.Name,
                }).ToList()
            }).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all trips");
            return [];
        }


    }
    //Inherited from ITripService
    public async Task<IEnumerable<Trip>> GetTripsAsync()
    {
        try
        {
            return await _context.Trips
                     .Include(t => t.Origin_City)
                     .Include(t => t.Destination_City)
                     .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting trips");
            return Enumerable.Empty<Trip>();
        }
     
    }
    //Inherited from ITripService
    public async Task<IEnumerable<Event>> GetEventsByTripIdAsync(long tripId)
    {
        try
        {
            var trip = await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null)
            {
                return Enumerable.Empty<Event>();
            }

            return await _context.Events
                .Where(e => e.EquipmentId == trip.EquipmentId && e.EventDate >= trip.Start_Date && e.EventDate <= trip.End_Date)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting events by trip id");
           return Enumerable.Empty<Event>();
        }
        
    }
}
