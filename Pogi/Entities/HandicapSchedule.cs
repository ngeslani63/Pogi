using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class HandicapSchedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HandicapScheduleId { get; set; }
        public int RevisionNumber { get; set; }
        [Display(Name = "Handicap Effective Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
