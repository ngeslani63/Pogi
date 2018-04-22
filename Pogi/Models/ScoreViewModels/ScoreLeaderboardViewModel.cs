using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreLeaderboardViewModel : Member
    {
        public Course Course { get; set; }
        public Score Score { get; set; }
        public String Merit { get; set; }
    }
}
