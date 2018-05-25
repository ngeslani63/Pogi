using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Pogi.Entities
{
    public class TeeAssign
    {
        public TeeAssign()
        {
            Group = 0;
            Order = 0;
            NoShow = false;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeeAssignId { get; set; }
        public int TeeTimeId { get; set; }
        public int MemberId { get; set; }
        public int PlayId { get; set; }
        public string GuestName { get; set; }
        public int Group { get; set; }
        public int Order { get; set; }
        [DefaultValue(false)]
        public bool NoShow { get; set; } 
        [DefaultValue(RecordState.Active)]
        public RecordState RecordStatus { get; set; }

        public string color { get; set;s }
    }
}
