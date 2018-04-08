using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.PlayerViewModels
{
    public class PlayerCreateViewModel
    {
        public PlayerCreateViewModel()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday);

            Player = new Player();
            Player.PlayDate = nextSaturday;
            MemberPlaying = true;
            GuestPlaying = false;

        }

        public List<SelectListItem> Members { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Member Playing")]
        public bool MemberPlaying { get; set; }
        [Display(Name = "Guest Playing")]
        public bool GuestPlaying { get; set; }

        public Player Player { get; set; }

    }
}
