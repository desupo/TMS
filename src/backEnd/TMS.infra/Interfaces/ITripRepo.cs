using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.domain.Entities;

namespace TMS.infra.Interfaces;

public interface ITripRepo
{
    Task AddTripsAsync(IEnumerable<Trip> trips);
}
