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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeeAssignId { get; set; }
        public int TeeTimeId { get; set; }
        public int MemberId { get; set; }
        public string GuestName { get; set; }
        public int Order { get; set; }
        [DefaultValue(false)]
        public bool NoShow { get; set; } = false;
        [DefaultValue(RecordState.Active)]
        public RecordState RecordStatus { get; set; }
    }
}
