using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class TeeTime
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeeTimeId { get; set; }
        public int ReservedById { get; set; }
        [Display(Name = "Date/Time")]
        public DateTime TeeTimeTS { get; set; }
        public int CourseId { get; set; }
        [Display(Name = "Number of Players")]
        [DefaultValue(4)]
        public int NumPlayers { get; set; }
        [DefaultValue(false)]
        public bool AutoAssign { get; set; }
    }
}
