using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.GalleryViewModel
{
    public class GalleryPerformersViewModel
    {
        public ScoreInfo LowNetWeekly { get; set; }
        public ScoreInfo LowNetMonth { get; set; }
        public ScoreInfo LowNetYear { get; set; }

        public ScoreInfo LowGrossWeekly { get; set; }
        public ScoreInfo LowGrossMonth { get; set; }
        public ScoreInfo LowGrossYear { get; set; }


    }
}
