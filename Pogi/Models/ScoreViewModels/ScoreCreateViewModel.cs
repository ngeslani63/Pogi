﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using Pogi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ScoreViewModels
{
    public class ScoreCreateViewModel
    {
        //private IDateTime _dateTime;

        public ScoreCreateViewModel()
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime today = (TimeZoneInfo.ConvertTime(DateTime.Now, est)).Date;
            int daysSinceSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek - 7) % 7;
            DateTime lastSaturday = today.AddDays(daysSinceSaturday);
            ScoreDate = lastSaturday;
        }
        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> Members { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> Tours { get; set; }

        public int ScoreId { get; set; }
        [Display(Name = "Score for Member")]
        public int MemberId { get; set; }
        public int CourseId { get; set; }
        public string Color { get; set; }

        public Member EnteredBy { get; set; }
        [Display(Name = "Score Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime ScoreDate { get; set; }
        [Display(Name = "01")]
        [Range(0,15)]
        public int Hole01 { get; set; }
        [Display(Name = "02")]
        [Range(0,15)]
        public int Hole02 { get; set; }
        [Display(Name = "03")]
        [Range(0,15)]
        public int Hole03 { get; set; }
        [Display(Name = "04")]
        [Range(0,15)]
        public int Hole04 { get; set; }
        [Display(Name = "05")]
        [Range(0,15)]
        public int Hole05 { get; set; }
        [Display(Name = "06")]
        [Range(0,15)]
        public int Hole06 { get; set; }
        [Display(Name = "07")]
        [Range(0,15)]
        public int Hole07 { get; set; }
        [Display(Name = "08")]
        [Range(0,15)]
        public int Hole08 { get; set; }
        [Display(Name = "09")]
        [Range(0,15)]
        public int Hole09 { get; set; }
        [Display(Name = "10")]
        [Range(0,15)]
        public int Hole10 { get; set; }
        [Display(Name = "11")]
        [Range(0,15)]
        public int Hole11 { get; set; }
        [Display(Name = "12")]
        [Range(0,15)]
        public int Hole12 { get; set; }
        [Display(Name = "13")]
        [Range(0,15)]
        public int Hole13 { get; set; }
        [Display(Name = "14")]
        [Range(0,15)]
        public int Hole14 { get; set; }
        [Display(Name = "15")]
        [Range(0,15)]
        public int Hole15 { get; set; }
        [Display(Name = "16")]
        [Range(0,15)]
        public int Hole16 { get; set; }
        [Display(Name = "17")]
        [Range(0,15)]
        public int Hole17 { get; set; }
        [Display(Name = "18")]
        [Range(0,15)]
        public int Hole18 { get; set; }
        [Display(Name = "In")]
        public int HoleIn { get; set; }
        [Display(Name = "Out")]
        public int HoleOut { get; set; }
        [Display(Name = "Total")]
        public int HoleTotal { get; set; }
        [Display(Name = "Is this a Tour Event")]
        [DefaultValue(false)]
        public bool TourEvent { get; set; }
        [Display(Name = "Tour Name")]
        public int TourId { get; set; }
        [Display(Name = "About Game")]
        [MaxLength(200)]
        public string AboutGame { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTs { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTs { get; set; }
        public ScoreType ScoreType { get; set; }

    }
}
