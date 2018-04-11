using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeAssignViewModels
{
    public class TeeAssignEditViewModel
    {
        public TeeTime TeeTime { get; set; }
        public Member ReservedBy { get; set; }
        public Course Course { get; set; }
        public List<string> PlayIds { get; set; }
        public List<SelectListItem> Players { get; set; }
    }
}
