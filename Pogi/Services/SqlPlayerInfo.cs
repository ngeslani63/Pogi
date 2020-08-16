using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<PlayerInfo> PlayerInfos = new List<PlayerInfo>();
            var confirmedPlayers = _context.Player.Where(r => r.PlayDate >= System.DateTime.Today && 
                r.Confirmed == true).OrderBy(r => r.PlayDate).ThenBy(r => r.ConfirmDate).ThenBy(r => r.PlayId);
            foreach (Player player in confirmedPlayers)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == player.MemberId);
                try
                {
                    PlayerInfo playerInfo = new PlayerInfo(player, member);
                    PlayerInfos.Add(playerInfo);
                }
                catch (Exception)
                {

                }
            }
            var withdrawnPlayers = _context.Player.Where(r => r.PlayDate >= System.DateTime.Today &&
                r.Withdrawn == true).OrderBy(r => r.PlayDate).ThenBy(r => r.ConfirmDate).ThenBy(r => r.PlayId);
            foreach (Player player in withdrawnPlayers)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == player.MemberId);
                try
                {
                    PlayerInfo playerInfo = new PlayerInfo(player, member);
                    PlayerInfos.Add(playerInfo);
                }
                catch (Exception)
                {

                }
            }
            var Players = _context.Player.Where(r => r.PlayDate >= System.DateTime.Today &&
                r.Withdrawn == false && r.Confirmed == false).OrderBy(r => r.PlayDate).ThenBy(r => r.ConfirmDate).ThenBy(r => r.PlayId);
            if (Players != null)
            {
                Dictionary<String, PlayerInfo> mapPlayerInfos = new Dictionary<string, PlayerInfo>();

                foreach (Player player in Players)
                {
                    Member member = _context.Member.FirstOrDefault(r => r.MemberId == player.MemberId);

                    PlayerInfo playerInfo = new PlayerInfo(player, member);
                    try
                    {
                        mapPlayerInfos.Add(member.LastName + member.FirstName, playerInfo);
                    }
                    catch (Exception)
                    {

                    }
                    // PlayerInfos.Add(playerInfo);
                }
                if (mapPlayerInfos.Count > 0)
                {
                    foreach (var kvp in mapPlayerInfos.OrderBy (kvp => kvp.Key))
                    {
                        try
                        {
                            PlayerInfos.Add(kvp.Value);
                        }
                        catch (Exception )
                        {

                        }
                    }
                }
            }
            return PlayerInfos;
        }
        public List<SelectListItem> getPlayers(DateTime dateTime)
        {
            DateTime date = dateTime.Date;
            DateTime datePlus = date.AddDays(1);
            List<SelectListItem> playerList = new List<SelectListItem>();
            SelectListItem sl = new SelectListItem { Text = "Unassigned", Value = "0" };
            playerList.Add(sl);
            IEnumerable<Player> players = _context.Player.Where(r => r.PlayDate >= date && r.PlayDate < datePlus && ! r.Withdrawn).OrderBy(r => r.PlayDate).ThenBy(r => r.ConfirmDate).ThenBy(r => r.PlayId);
            foreach (Player player in players)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == player.MemberId);
                string playerName = member.FirstName + " " + member.LastName;
                string value = player.PlayId.ToString();
                if (player.GuestName != null && player.GuestName.Length > 0)
                {
                    playerName = player.GuestName;
                }
                sl = new SelectListItem { Text = playerName, Value = value };
                playerList.Add(sl);
            }
            return playerList;
        }




    }
}
