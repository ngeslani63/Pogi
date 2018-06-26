using Microsoft.AspNetCore.Mvc.Rendering;
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
        List<SelectListItem> getTours(bool add0);
        Tour getTour(int TourId);
        Tour getTour(string TourName);
        Tour getTourOnDate(DateTime TourDate);

        Tour getLatestTour();
    }
}
