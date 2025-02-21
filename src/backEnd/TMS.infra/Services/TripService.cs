using Microsoft.EntityFrameworkCore;
using TMS.infra.Persistence.Context;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.domain.Entities;
namespace TMS.infra.Services;

public class TripService : ITripService
{
    private readonly dbContext _context;

    public TripService(dbContext context)
    {
        _context = context;
    }

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
            return new TripDetailsDto();
        }
    }

    public async Task<List<TripDetailsDto>> GetTripsByEquipmentIdAsync(string equipmentId)
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
            return [];
        }
        

    }

    public async Task<IEnumerable<Trip>> GetTripsAsync()
    {
        return await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByTripIdAsync(long tripId)
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
}
