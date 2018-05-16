using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class TourDay
    {
        public TourDay()
        {
            TourDate = DateTime.Today;
            TourId = 0;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourDayId { get; set; }
        public int TourId { get; set; }
        [Display(Name = "Tour Date")]
        [DataType(DataType.Date)]
        public DateTime TourDate { get; set; }
    }
}
