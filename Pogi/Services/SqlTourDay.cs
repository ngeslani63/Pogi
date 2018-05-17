using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlTourDay : ITourDay
    {
        private PogiDbContext _context;

        public SqlTourDay(PogiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TourDay> getForTour(int TourId)
        {
            return _context.TourDay.Where(r => r.TourId == TourId).OrderByDescending(r => r.TourDate);
        }
    }
}
