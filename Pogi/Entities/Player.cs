using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pogi.Entities
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayId { get; set; }
        public int MemberId { get; set; }
        public string GuestName { get; set; }
        public DateTime PlayDate { get; set; }
        [DefaultValue(0)]
        public int Order { get; set; }
        [DefaultValue(0)]
        public int preferTeeTimeId1 { get; set; }
        [DefaultValue(0)]
        public int preferTeeTimeId2 { get; set; }
        [DefaultValue(0)]
        public int preferTeeTimeId3 { get; set; }
    }
}
