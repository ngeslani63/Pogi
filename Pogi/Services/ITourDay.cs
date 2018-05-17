using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ITourDay
    {
        IEnumerable<TourDay> getForTour(int TourId);
    }
}
