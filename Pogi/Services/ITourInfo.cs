﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ITourInfo
    {
        List<SelectListItem> getTours();
        Tour getTour(int TourId);
        Tour getTourOnDate(DateTime TourDate);
    }
}
