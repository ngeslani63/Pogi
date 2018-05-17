using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TourDayViewModels
{
    public class TourDayIndexViewModel
    {
        public Tour Tour { get; set; }
        public IEnumerable<TourDay> TourDays { get; set; }
    }
}
