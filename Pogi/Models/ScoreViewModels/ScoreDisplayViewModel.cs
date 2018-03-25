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

        public Member User { get; set; }
    }
}
