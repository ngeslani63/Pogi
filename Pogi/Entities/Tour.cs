using Pogi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Tour
    {
        public Tour()
        {
            HcpAllowPct = 80.0F;
            TourType = TourType.SingleDay;
            ScorerType = ScorerType.Regular;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        [Display(Name = "Tour Name")]
        [Required, MaxLength(80)]
        public string CourseName { get; set; }
        [Display(Name = "Handicap Allowance")]
        [Required]
        [Range(50.0, 100.0)]
        public float HcpAllowPct { get; set; }
        [Display(Name = "Tour Date")]
        TourType TourType { get; set; }
        [DataType(DataType.DateTime)]
        ScorerType ScorerType { get; set; }
        public DateTime TourDate { get; set; }
    }
}
