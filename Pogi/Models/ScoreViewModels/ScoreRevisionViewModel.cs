using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreRevisionViewModel
    {
        public List<SelectListItem> ActiveDates;
        public IEnumerable<ScoreInfo> ScoreInfos { get; set; }
        [Display(Name = "Revision Effective Date")]
        public string EffDate;


    }
}
