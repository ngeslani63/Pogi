﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        List<SelectListItem> getSelectList(int TourId);
        List<SelectListItem> getSelectList(int TourId, DateTime TourDate);
        List<SelectListItem> getSelectList(int TourId, DateTime TourDate, Boolean filterFuture);
        TourDay GetLatestTourDay(int TourId);
    }
}
