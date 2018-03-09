using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScoreId { get; set; }
        public int MemberId { get; set; }
        public int CourseId { get; set; }
        public string Color { get; set; }
        public int EnteredById { get; set; }
        public DateTime ScoreTS { get; set; }
        public DateTime AttestedTS { get; set; }
        public int TeeTimeId { get; set; }
        public int Hole01 { get; set; }
        [Display(Name = "02")]
        public int Hole02 { get; set; }
        [Display(Name = "03")]
        public int Hole03 { get; set; }
        [Display(Name = "04")]
        public int Hole04 { get; set; }
        [Display(Name = "05")]
        public int Hole05 { get; set; }
        [Display(Name = "06")]
        public int Hole06 { get; set; }
        [Display(Name = "07")]
        public int Hole07 { get; set; }
        [Display(Name = "08")]
        public int Hole08 { get; set; }
        [Display(Name = "09")]
        public int Hole09 { get; set; }
        [Display(Name = "10")]
        public int Hole10 { get; set; }
        [Display(Name = "11")]
        public int Hole11 { get; set; }
        [Display(Name = "12")]
        public int Hole12 { get; set; }
        [Display(Name = "13")]
        public int Hole13 { get; set; }
        [Display(Name = "14")]
        public int Hole14 { get; set; }
        [Display(Name = "15")]
        public int Hole15 { get; set; }
        [Display(Name = "16")]
        public int Hole16 { get; set; }
        [Display(Name = "17")]
        public int Hole17 { get; set; }
        [Display(Name = "18")]
        public int Hole18 { get; set; }
        [Display(Name = "In")]
        public int HoleIn { get; set; }
        [Display(Name = "Out")]
        public int HoleOut { get; set; }
        [Display(Name = "Total")]
        public int HoleTotal { get; set; }

    }
}
