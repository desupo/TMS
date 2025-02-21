using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.domain.Entities;
using TMS.infra.Interfaces;
using TMS.infra.Persistence.Context;

namespace TMS.infra.Persistence.Repositories;

public class TripRepo : ITripRepo
{
    private readonly dbContext _context;

    public TripRepo(dbContext context)
    {
        _context = context;
    }

    public async Task AddTripsAsync(IEnumerable<Trip> trips)
    {
        await _context.Trips.AddRangeAsync(trips);
        await _context.SaveChangesAsync();
    }
}
