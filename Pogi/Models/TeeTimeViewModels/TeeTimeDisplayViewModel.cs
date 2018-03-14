using Pogi.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pogi.Models.TeeTimeViewModels
{
    public class TeeTimeDisplayViewModel
    {
        public TeeTimeDisplayViewModel()
        {
        }
            public Member Member { get; set; }
            public int TeeTimeId { get; set; }

            [Display(Name = "Tee Date and Time")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
            public DateTime TeeTimeTS { get; set; }
             [Display(Name = "Course Name")]
            public string CourseName { get; set; }
             [Display(Name = "Number of Players")]
            public int NumPlayers { get; set; }

    }
}