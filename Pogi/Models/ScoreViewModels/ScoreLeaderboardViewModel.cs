using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreLeaderboardViewModel : Member
    {
        [Display(Name = "As Of Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime AsOfDate { get; set; }
        public bool Weekly { get; set; }
        public bool Monthly { get; set; }
        public bool Yearly { get; set; }
        public bool AllTime { get; set; }
        public List<ScoreInfo> ScoreInfos { get; set; }
    }
}
