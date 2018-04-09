using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlPlayerInfo : IPlayerInfo
    {
        private PogiDbContext _context;

        public SqlPlayerInfo(PogiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<PlayerInfo> getRoster()
        {
            var Players = _context.Player.Where(r => r.PlayDate >= System.DateTime.Today).OrderBy(r => r.PlayDate);
            List<PlayerInfo> PlayerInfos = new List<PlayerInfo>();
            foreach (Player player in Players)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == player.MemberId);

                PlayerInfo playerInfo = new PlayerInfo(player, member);
                PlayerInfos.Add(playerInfo);
            }
            return PlayerInfos;
        }


    }
}
