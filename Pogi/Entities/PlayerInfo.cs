using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class PlayerInfo
    {
        public PlayerInfo(Player player, Member member)
        {
            Player = player;
            Member = member;
        }
        public Player Player { get; set; }
       public Member Member { get; set; }
   
    }
}
