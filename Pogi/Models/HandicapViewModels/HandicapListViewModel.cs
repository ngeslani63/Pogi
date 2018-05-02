using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.HandicapViewModels
{
    public class HandicapListViewModel
    {
        public List<SelectListItem> ActiveDates;

        public IEnumerable<HandicapInfo> HandicapInfos;
        [Display(Name = "Handicap Effective Date")]
        public string Date;

    }
}
