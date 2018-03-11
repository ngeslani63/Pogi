using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pogi.Entities
{
    public class Player
    {
        public Player()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday);
            PlayDate = nextSaturday;
            GuestName = "";
            Order = 0;
            preferTeeTimeId1 = 0;
            preferTeeTimeId2 = 0;
            preferTeeTimeId3 = 0;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayId { get; set; }
        public int MemberId { get; set; }
        public string GuestName { get; set; }
        [DataType(DataType.Date)]
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
