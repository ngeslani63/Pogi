using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models
{
    public class PlayerExpanded : Player
    {
        public PlayerExpanded(Player player)
        {
            this.PlayId = player.PlayId;
        }
    }
}
