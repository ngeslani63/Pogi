using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IPlayerInfo
    {
        IEnumerable<PlayerInfo> getRoster();
        List<SelectListItem> getPlayers(DateTime dateTime);
    }
}
