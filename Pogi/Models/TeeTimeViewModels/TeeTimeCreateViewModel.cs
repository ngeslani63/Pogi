using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeTimeViewModels
{
    public class TeeTimeCreateViewModel
    {
        public TeeTimeCreateViewModel()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday);

            TeeTime = new TeeTime();
            TeeTime.TeeTimeTS = nextSaturday;
        }

        public TeeTime TeeTime { get; set; }
        public Member Member { get; set; }
        public Course Course { get; set; }

        public IEnumerable<Member> Courses { get; set; }

    }
}
