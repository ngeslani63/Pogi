using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class HandicapSchedule
    {
        public HandicapSchedule()
        {
            RecordStatus = RecordState.Active;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HandicapScheduleId { get; set; }
        public int RevisionNumber { get; set; }
        [Display(Name = "Handicap Effective Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Record Status")]
        [DefaultValue(RecordState.Active)]
        public RecordState RecordStatus { get; set; }


    }
}
