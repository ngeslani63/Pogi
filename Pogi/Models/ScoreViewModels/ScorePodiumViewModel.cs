using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScorePodiumViewModel
    {
        public IEnumerable<ScoreInfo> ScoreInfos { get; set; }

        public List<SelectListItem> Tours { get; set; }

        public string TourId { get; set; }

        public Member User { get; set; }
    }
}
