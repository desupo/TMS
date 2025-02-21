using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.application.DTOs;
using TMS.application.Interfaces;
using TMS.domain.Entities;
using TMS.infra.Interfaces;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Services;

public class EquipmentEventService(dbContext context, ITripRepo tripRepo) : IEquipmentEventService
{
    private readonly dbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ITripRepo _tripRepo = tripRepo ?? throw new ArgumentNullException(nameof(tripRepo));

    public async Task<IEnumerable<Event>> ParseCsvAsync(IFormFile file)
    {
        var events = new List<Event>();

        using (var stream = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
        {
            var csvRecords = csv.GetRecords<Event>().ToList();
            foreach (var record in csvRecords)
            {
                var city = await _context.Cities.FindAsync(record.CityId);
                if (city != null)
                {
                    var eventTimeZone = TimeZoneInfo.FindSystemTimeZoneById(city.TimeZone);
                    record.EventDate = TimeZoneInfo.ConvertTimeToUtc(record.EventDate.DateTime, eventTimeZone);
                }
                events.Add(record);
            }
        }
        return events;
    }

    public async Task ProcessEventsAsync(IEnumerable<Event> events)
    {
        var groupedEvents = events.GroupBy(e => e.EquipmentId);

        foreach (var group in groupedEvents)
        {
            var equipmentId = group.Key;
            var eventList = group.OrderBy(e => e.EventDate).ToList();
            Trip currentTrip = null;

            for (int i = 0; i < eventList.Count; i++)
            {
                var eventEntity = eventList[i];

                if (eventEntity.Code == "W")
                {
                    if (currentTrip == null)
                    {
                        currentTrip = new Trip
                        {
                            EquipmentId = equipmentId,
                            Origin_CityId = eventEntity.CityId,
                            Start_Date = eventEntity.EventDate,
                            HasIssue = false,
                            Completed = false
                        };
                    }
                    else
                    {
                        // Misplaced code, flag the trip with an issue
                        currentTrip.HasIssue = true;
                    }
                }
                else if (eventEntity.Code == "Z")
                {
                    if (currentTrip != null)
                    {
                        currentTrip.Destination_CityId = eventEntity.CityId;
                        currentTrip.End_Date = eventEntity.EventDate;
                        currentTrip.Completed = true;
                        _context.Trips.Add(currentTrip);
                        currentTrip = null;
                    }
                    else
                    {
                        // Misplaced code, flag as issue
                        if (i == 0 || eventList[i - 1].Code != "Z")
                        {
                            currentTrip = new Trip
                            {
                                EquipmentId = equipmentId,
                                Origin_CityId = eventEntity.CityId,
                                Start_Date = eventEntity.EventDate,
                                HasIssue = true,
                                Completed = false
                            };
                        }
                    }
                }
            }

            if (currentTrip != null && !currentTrip.Completed)
            {
                // Trip ended without a matching "Z" code
                currentTrip.HasIssue = true;
                _context.Trips.Add(currentTrip);
            }
        }

        await _context.SaveChangesAsync();
    }
    public async Task ProcessEventsAsync(IEnumerable<EquipmentEvent> events)
    {
        var trips = new List<Trip>();
        Trip currentTrip = null;

        foreach (var evt in events.OrderBy(e => e.EventTime))
        {
            if (evt.EventCode == "W")
            {
                if (currentTrip != null)
                {
                    currentTrip.HasIssue = true;
                    trips.Add(currentTrip);
                }
                currentTrip = new Trip
                {
                    EquipmentId = evt.EquipmentId,
                    Origin_CityId = evt.CityId,
                    Start_Date = evt.EventTime,
                    Completed = false
                };
            }
            else if (evt.EventCode == "Z" && currentTrip != null)
            {
                currentTrip.Destination_CityId = evt.CityId;
                currentTrip.End_Date = evt.EventTime;
                //currentTrip.Duration = currentTrip.End_Date - currentTrip.Start_Date;
                currentTrip.Completed = true;
                trips.Add(currentTrip);
                currentTrip = null;
            }
            else if (currentTrip != null)
            {
                currentTrip.HasIssue = true;
            }
        }

        if (currentTrip != null)
        {
            trips.Add(currentTrip);
        }

        _context.Trips.AddRange(trips);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsAsync()
    {
        return await _context.Trips.ToListAsync();
    }

    public async Task<IEnumerable<EquipmentEvent>> GetEventsByTripIdAsync(long tripId)
    {
        var trip = await _context.Trips
            .Include(t => t.Origin_City)
            .Include(t => t.Destination_City)
            .FirstOrDefaultAsync(t => t.Id == tripId);

        if (trip == null)
        {
            return Enumerable.Empty<EquipmentEvent>();
        }

        return await _context.EquipmentEvents
            .Where(e => e.EquipmentId == trip.EquipmentId && e.EventTime >= trip.Start_Date && e.EventTime <= trip.End_Date)
            .OrderBy(e => e.EventTime)
            .ToListAsync();
    }
}
