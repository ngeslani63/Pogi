using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeAssignViewModels
{
    public class TeeAssignViewModel
    {
        public IEnumerable<TeeAssignInfo> TeeAssignInfos { get; set; }
    }
}
