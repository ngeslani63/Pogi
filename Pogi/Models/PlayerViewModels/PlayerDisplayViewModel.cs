using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.PlayerViewModels
{
    public class PlayerDisplayViewModel
    {
        public Member EnteredBy { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Member Playing")]
        public bool MemberPlaying { get; set; }
        [Display(Name = "Guest Playing")]
        public bool GuestPlaying { get; set; }

        public Player Player { get; set; }
    }
}
