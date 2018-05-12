using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlTourInfo : ITourInfo
    {
        private PogiDbContext _context;

        public SqlTourInfo(PogiDbContext context)
        {
            _context = context;
        }
        public Tour getTour(int TourId)
        {
            return _context.Tour.FirstOrDefault(r => r.TourId == TourId);
        }

        public Tour getTourOnDate(DateTime TourDate)
        {
            return _context.Tour.FirstOrDefault(r => r.TourDate == TourDate);
        }

        public List<SelectListItem> getTours()
        {
            List<SelectListItem> ToursList = new List<SelectListItem>();
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysSinceSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek - 7) % 7;
            DateTime lastSaturday = today.AddDays(daysSinceSaturday).Date;
            IEnumerable<Tour> Tours = _context.Tour.OrderBy(r => r.TourDate);
            foreach (Tour tour in Tours)
            {
                var selected = false;
                if (tour.TourDate.Equals(lastSaturday)) selected = true;
                SelectListItem sl = new SelectListItem { Text = tour.TourName, Value = tour.TourId.ToString(), Selected = selected };
                ToursList.Add(sl);
            }
            return ToursList;
        }
    }
}
