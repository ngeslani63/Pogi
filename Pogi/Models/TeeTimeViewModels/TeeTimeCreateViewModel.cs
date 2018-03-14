using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeTimeViewModels
{
    public class TeeTimeCreateViewModel
    {
        public TeeTimeCreateViewModel()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday).AddHours(06).AddMinutes(00);
 
            TeeTimeTS = nextSaturday;
            NumPlayers = 4;
        }

        public Member Member { get; set; }
        public List<SelectListItem> Courses { get; set; }

        public int TeeTimeId { get; set; }
        public int ReservedById { get; set; }
        [Required]
        [Display(Name = "Tee Date and Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public DateTime TeeTimeTS { get; set; }
        [Display(Name = "Course Name")]
        public int CourseId { get; set; }
        [Display(Name = "Number of Players")]
        [DefaultValue(4)]
        [Range(1,40)]
        public int NumPlayers { get; set; }
    }


}
