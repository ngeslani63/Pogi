using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreDisplayViewModel
    {
        public ScoreDisplayViewModel()
        {
            Member User = new Member();
        }
        public IEnumerable<ScoreInfo> ScoreInfos { get; set; }

        public List<SelectListItem> Tours { get; set; }

        public Member User { get; set; }
        public string Search { get; set; }
        public string TourId { get; set; }
    }
}
