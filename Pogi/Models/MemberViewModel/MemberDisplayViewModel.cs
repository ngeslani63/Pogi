using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.MemberViewModel
{
    public class MemberDisplayViewModel
    {
        public MemberDisplayViewModel()
        {
            dispMembers = true;
            dispGuests = false;
        }
        public bool dispMembers { get; set; }

        public bool dispGuests { get; set; }

        public IEnumerable<Pogi.Entities.Member> Members { get; set; }
    }
}
