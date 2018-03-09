using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeTimeViewModels
{
    public class TeeTimeViewModel
    {
        public IEnumerable<TeeTimeInfo> TeeTimeInfos { get; set; }
        public IEnumerable<PlayerInfo> PlayerInfos { get; set; }
        //public IEnumerable<TeeAssignInfo> TeeAssignInfos { get; set; }
    }
}
