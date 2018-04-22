using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.HandicapViewModels
{
    public class HandicapEditViewModel : Handicap
    {

        public Member Member { get; set; }
    }
}
