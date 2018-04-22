using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.HandicapViewModels
{
    public class HandicapIndexViewModel
    {
        public Member Member { get; set; }
        public IEnumerable<Handicap> Handicaps { get; set; }
   
    }
}
