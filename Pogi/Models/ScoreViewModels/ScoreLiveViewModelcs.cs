using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreLiveViewModelcs
    {
        public Course Course { get; set; }
        public TeeTime TeeTime { get; set; }
        
        List<Score> Scores { get; set; }
    }
}
