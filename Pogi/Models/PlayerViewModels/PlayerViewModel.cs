using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.PlayerViewModels
{
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday);

            Player = new Player();
            Player.PlayDate = nextSaturday;
            PlayerPlaying = true;
            GuestPlaying = false;

        }

        public IEnumerable<Member> Members { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Member Playing")]
        public bool PlayerPlaying { get; set; }
        [Display(Name = "Guest Playing")]
        public bool GuestPlaying { get; set; }

        public Player Player { get; set; }

    }
}
