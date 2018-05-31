using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{

    public class CourseHandicap
    {
        public CourseHandicap()
        {
            MenHcp01 = 1;
            MenHcp02 = 1;
            MenHcp03 = 1;
            MenHcp04 = 1;
            MenHcp05 = 1;
            MenHcp06 = 1;
            MenHcp07 = 1;
            MenHcp08 = 1;
            MenHcp09 = 1;
            MenHcp10 = 1;
            MenHcp11 = 1;
            MenHcp12 = 1;
            MenHcp13 = 1;
            MenHcp14 = 1;
            MenHcp15 = 1;
            MenHcp16 = 1;
            MenHcp17 = 1;
            MenHcp18 = 1;
            LadiesHcp01 = 1;
            LadiesHcp02 = 1;
            LadiesHcp03 = 1;
            LadiesHcp04 = 1;
            LadiesHcp05 = 1;
            LadiesHcp06 = 1;
            LadiesHcp07 = 1;
            LadiesHcp08 = 1;
            LadiesHcp09 = 1;
            LadiesHcp10 = 1;
            LadiesHcp11 = 1;
            LadiesHcp12 = 1;
            LadiesHcp13 = 1;
            LadiesHcp14 = 1;
            LadiesHcp15 = 1;
            LadiesHcp16 = 1;
            LadiesHcp17 = 1;
            LadiesHcp18 = 1;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseHandicapId { get; set; }
        public int CourseId { get; set; }
        [Display(Name = "Men's Handicap 01")]
        public int MenHcp01 { get; set; }
        [Display(Name = "Men's Handicap 02")]
        public int MenHcp02 { get; set; }
        [Display(Name = "Men's Handicap 03")]
        public int MenHcp03 { get; set; }
        [Display(Name = "Men's Handicap 04")]
        public int MenHcp04 { get; set; }
        [Display(Name = "Men's Handicap 05")]
        public int MenHcp05 { get; set; }
        [Display(Name = "Men's Handicap 06")]
        public int MenHcp06 { get; set; }
        [Display(Name = "Men's Handicap 07")]
        public int MenHcp07 { get; set; }
        [Display(Name = "Men's Handicap 08")]
        public int MenHcp08 { get; set; }
        [Display(Name = "Men's Handicap 09")]
        public int MenHcp09 { get; set; }
        [Display(Name = "Men's Handicap 10")]
        public int MenHcp10 { get; set; }
        [Display(Name = "Men's Handicap 11")]
        public int MenHcp11 { get; set; }
        [Display(Name = "Men's Handicap 12")]
        public int MenHcp12{ get; set; }
        [Display(Name = "Men's Handicap 13")]
        public int MenHcp13 { get; set; }
        [Display(Name = "Men's Handicap 14")]
        public int MenHcp14 { get; set; }
        [Display(Name = "Men's Handicap 15")]
        public int MenHcp15 { get; set; }
        [Display(Name = "Men's Handicap 16")]
        public int MenHcp16 { get; set; }
        [Display(Name = "Men's Handicap 17")]
        public int MenHcp17 { get; set; }
        [Display(Name = "Men's Handicap 18")]
        public int MenHcp18 { get; set; }
        [Display(Name = "Ladies Handicap 01")]
        public int LadiesHcp01 { get; set; }
        [Display(Name = "Ladies Handicap 02")]
        public int LadiesHcp02 { get; set; }
        [Display(Name = "Ladies Handicap 03")]
        public int LadiesHcp03 { get; set; }
        [Display(Name = "Ladies Handicap 04")]
        public int LadiesHcp04 { get; set; }
        [Display(Name = "Ladies Handicap 05")]
        public int LadiesHcp05 { get; set; }
        [Display(Name = "Ladies Handicap 06")]
        public int LadiesHcp06 { get; set; }
        [Display(Name = "Ladies Handicap 07")]
        public int LadiesHcp07 { get; set; }
        [Display(Name = "Ladies Handicap 08")]
        public int LadiesHcp08 { get; set; }
        [Display(Name = "Ladies Handicap 09")]
        public int LadiesHcp09 { get; set; }
        [Display(Name = "Ladies Handicap 10")]
        public int LadiesHcp10 { get; set; }
        [Display(Name = "Ladies Handicap 11")]
        public int LadiesHcp11 { get; set; }
        [Display(Name = "Ladies Handicap 12")]
        public int LadiesHcp12 { get; set; }
        [Display(Name = "Ladies Handicap 13")]
        public int LadiesHcp13 { get; set; }
        [Display(Name = "Ladies Handicap 14")]
        public int LadiesHcp14 { get; set; }
        [Display(Name = "Ladies Handicap 15")]
        public int LadiesHcp15 { get; set; }
        [Display(Name = "Ladies Handicap 16")]
        public int LadiesHcp16 { get; set; }
        [Display(Name = "Ladies Handicap 17")]
        public int LadiesHcp17 { get; set; }
        [Display(Name = "Ladies Handicap 18")]
        public int LadiesHcp18 { get; set; }

    }
}
