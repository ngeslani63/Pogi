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
            TourDate = DateTime.Today;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        [Display(Name = "Tour Name")]
        [Required, MaxLength(80)]
        public string TourName { get; set; }
        [Display(Name = "Handicap Allowance")]
        [Required]
        [Range(50.0, 100.0)]
        public float HcpAllowPct { get; set; }
        [Display(Name = "Tour Duration")]
        public TourType TourType { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Scorer Engine")]
        public ScorerType ScorerType { get; set; }
        [Display(Name = "Tour Date")]
        [DataType(DataType.Date)]
        public DateTime TourDate { get; set; }
    }
}
