using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.LiveViewModels
{
    public class LiveMatchBoardViewModel
    {

        public LiveMatchBoardViewModel()
        {
            Member User = new Member();
        }
        public Tour Tour { get; set; }
        public IEnumerable<ScoreInfo> ScoreInfos { get; set; }

        public Member User { get; set; }
        public string Search { get; set; }
        public string TourId { get; set; }
        public string TourDate { get; set; }
    }
}
