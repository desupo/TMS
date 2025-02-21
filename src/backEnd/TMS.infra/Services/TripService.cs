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
            .Include(t => t.Events)
            .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null)
            {
                return null;
            }

            var tripDetails = new TripDetailsDto
            {
                EquipmentId = trip.EquipmentId,
                OriginCity = trip.Origin_City.Name,
                DestinationCity = trip.Destination_City.Name,
                StartDate = trip.Start_Date,
                EndDate = trip.End_Date,
                Duration = trip.Duration,
                HasIssue = trip.HasIssue,
                Completed = trip.Completed,
                Events = trip.Events.OrderBy(e => e.EventDate).ToList()
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
            .Where(t => t.EquipmentId == equipmentId)
            .ToListAsync();

        return trips.Select(trip => new TripDetailsDto
        {
            EquipmentId = trip.EquipmentId,
            OriginCity = trip.Origin_City.Name,
            DestinationCity = trip.Destination_City.Name,
            StartDate = trip.Start_Date,
            EndDate = trip.End_Date,
            Duration = trip.Duration,
            HasIssue = trip.HasIssue,
            Completed = trip.Completed,
            Events = trip.Events.OrderBy(e => e.EventDate).ToList()
        }).ToList();
    }

    public async Task<List<TripDetailsDto>> GetAllTripsAsync()
    {
        var trips = await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .ToListAsync();

        return trips.Select(trip => new TripDetailsDto
        {
            EquipmentId = trip.EquipmentId,
            OriginCity = trip.Origin_City.Name,
            DestinationCity = trip.Destination_City.Name,
            StartDate = trip.Start_Date,
            EndDate = trip.End_Date,
            Duration = trip.Duration,
            HasIssue = trip.HasIssue,
            Completed = trip.Completed,
            Events = trip.Events.OrderBy(e => e.EventDate).ToList()
        }).ToList();

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
