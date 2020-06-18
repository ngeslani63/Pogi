using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.LiveViewModels
{
    public class LiveSelectViewModel
    {
        public LiveSelectViewModel()
        {
            Member User = new Member();
        }
        public List<SelectListItem> Tours { get; set; }
        public List<SelectListItem> TourDates { get; set; }
        public TeeTimeInfo TeeTimeInfo { get; set; }
        public Member User { get; set; }
        public string Search { get; set; }
        public string TourId { get; set; }
        public string TourDate { get; set; }
    }
}
