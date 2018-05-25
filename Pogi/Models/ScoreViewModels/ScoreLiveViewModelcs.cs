using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreLiveViewModelcs
    {
        public ScoreLiveViewModelcs()
        {
            
        }
        public Course Course { get; set; }
        public TeeTime TeeTime { get; set; }

        HashTable TeeAssigns { get; set; } // contain TeeAssigns for the TeeTime, keyed by group#,order#
        
        HashTable Scores { get; set; } // contain all Scores for the TeeTime, keyed by group#, order#

        HashTable Members { get; set; } // contain all Members for the TeeTime, keyed by group#, order#
    }
}
