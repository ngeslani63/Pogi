using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlTourDay : ITourDay
    {
        private PogiDbContext _context;
        private IDateTime _dateTime;

        public SqlTourDay(PogiDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public IEnumerable<TourDay> getForTour(int TourId)
        {
            return _context.TourDay.Where(r => r.TourId == TourId).OrderByDescending(r => r.TourDate);
        }

        public TourDay GetLatestTourDay(int TourId)
        {
            DateTime today = _dateTime.getToday();

            TourDay tourDay = _context.TourDay.Where(r => r.TourId == TourId && r.TourDate <= today).OrderByDescending(r => r.TourDate).FirstOrDefault();
            if (tourDay == null)
            {
                tourDay = _context.TourDay.Where(r => r.TourId == TourId && r.TourDate > today).OrderBy(r => r.TourDate).FirstOrDefault();
            }
            return tourDay;
        }

        public List<SelectListItem> getSelectList(int TourId)
        {
            DateTime today = _dateTime.getToday();
            bool selected = false;
            List<SelectListItem> tourDayList = new List<SelectListItem>();

            Tour tour = _context.Tour.FirstOrDefault(r => r.TourId == TourId);
            if (tour.TourType == Models.TourType.SingleDay)
            {
                SelectListItem sl = new SelectListItem { Text = tour.TourDate.ToShortDateString(), Value = tour.TourDate.ToShortDateString() };
                sl.Selected = true;
                tourDayList.Add(sl);
                return tourDayList;
            }

            IEnumerable<TourDay> tourDays = _context.TourDay.Where(r => r.TourId == TourId).OrderByDescending(r => r.TourDate);
            foreach (TourDay tourDay in tourDays)
            {
                SelectListItem sl = new SelectListItem { Text = tourDay.TourDate.ToShortDateString(), Value = tourDay.TourDate.ToShortDateString() };
                if (tourDay.TourDate <= today && selected == false)
                {
                    sl.Selected = true;
                    selected = true;
                }
                tourDayList.Add(sl);
            }
            return tourDayList;
        }
       
    }
}
