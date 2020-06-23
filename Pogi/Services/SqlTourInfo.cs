using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;

namespace Pogi.Services
{
    public class SqlTourInfo : ITourInfo
    {
        private IDateTime _dateTime;
        private PogiDbContext _context;

        public SqlTourInfo(PogiDbContext context, IDateTime dateTime)
        {
            _dateTime = dateTime;
            _context = context;
        }

        public Tour getLatestTour()
        {
            return _context.Tour.Where(r => r.TourType == TourType.SingleDay
                && r.TourDate <= _dateTime.getToday().AddDays(6)).OrderByDescending(r => r.TourDate).FirstOrDefault();
        }

        public Tour getTour(int TourId)
        {
            return _context.Tour.FirstOrDefault(r => r.TourId == TourId);
        }
        public Tour getTour(string TourName)
        {
            return _context.Tour.FirstOrDefault(r => r.TourName == TourName);
        }

        public Tour getTourOnDate(DateTime TourDate)
        {
            return _context.Tour.FirstOrDefault(r => r.TourDate == TourDate);
        }
        public List<SelectListItem> getTours()
        {
            return getTours(false, true);
        }
        public List<SelectListItem> getTours(bool add0)
        {
            return getTours(add0, false);
        }
        public List<SelectListItem> getTours(bool add0, bool thisYear)
        {
            List<SelectListItem> ToursList = new List<SelectListItem>();
            if (add0)
            {
                SelectListItem sl = new SelectListItem { Text = "All Scores", Value = "0" };
                ToursList.Add(sl);
            }
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysSinceSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek - 7) % 7;
            DateTime lastSaturday = today.AddDays(daysSinceSaturday).Date;
            int year = DateTime.Now.Year;
            DateTime firstDayofYear = new DateTime(year, 1, 1);
            IEnumerable<Tour> Tours;
            if (thisYear)
            {
                Tours = _context.Tour.Where(r => r.TourDate >= firstDayofYear).OrderBy(r => r.TourDate);
            }
            else
            {
                Tours = _context.Tour.Where(r => r.TourDate >= lastSaturday).OrderBy(r => r.TourDate);
            }
            foreach (Tour tour in Tours)
            {
                var selected = false;
                //if (tour.TourDate.Equals(lastSaturday)) selected = true;
                SelectListItem sl = new SelectListItem { Text = tour.TourName, Value = tour.TourId.ToString(), Selected = selected };
                ToursList.Add(sl);
            }
            return ToursList;
        }
    }
}
