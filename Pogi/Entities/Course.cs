﻿using Pogi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Course
    {
        public Course()
        {
            State = StateCode.NJ;
            Country = "USA";
            NumTees = 3;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Display(Name = "Golf Course")]
        [Required, MaxLength(80)]
        public string CourseName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [DefaultValue(StateCode.NJ)]
        public StateCode State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        [Display(Name = "01")]
        [Range(3, 6)]
        public int Par01 { get; set; }
        [Display(Name = "02")]
        [Range(3, 6)]
        public int Par02 { get; set; }
        [Display(Name = "03")]
        [Range(3, 6)]
        public int Par03 { get; set; }
        [Display(Name = "04")]
        [Range(3, 6)]
        public int Par04 { get; set; }
        [Display(Name = "05")]
        [Range(3, 6)]
        public int Par05 { get; set; }
        [Display(Name = "06")]
        [Range(3, 6)]
        public int Par06 { get; set; }
        [Display(Name = "07")]
        [Range(3, 6)]
        public int Par07 { get; set; }
        [Display(Name = "08")]
        [Range(3, 6)]
        public int Par08 { get; set; }
        [Display(Name = "09")]
        [Range(3, 6)]
        public int Par09 { get; set; }
        [Display(Name = "10")]
        [Range(3, 6)]
        public int Par10 { get; set; }
        [Display(Name = "11")]
        [Range(3, 6)]
        public int Par11 { get; set; }
        [Display(Name = "12")]
        [Range(3, 6)]
        public int Par12 { get; set; }
        [Display(Name = "13")]
        [Range(3, 6)]
        public int Par13 { get; set; }
        [Display(Name = "14")]
        [Range(3, 6)]
        public int Par14 { get; set; }
        [Display(Name = "15")]
        [Range(3, 6)]
        public int Par15 { get; set; }
        [Display(Name = "16")]
        [Range(3, 6)]
        public int Par16 { get; set; }
        [Display(Name = "17")]
        public int Par17 { get; set; }
        [Display(Name = "18")]
        [Range(3, 6)]
        public int Par18 { get; set; }
        [Display(Name = "In")]
        public int ParIn { get; set; }
        [Display(Name = "Out")]
        public int ParOut { get; set; }
        [Display(Name = "Total")]
        public int ParTotal { get; set; }

        [Display(Name = "Number of Tees")]
        [Range(1,9)]
        public int NumTees { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTs { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTs { get; set; }
    }
}
