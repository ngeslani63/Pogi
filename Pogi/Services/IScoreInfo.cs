using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IScoreInfo
    {
        List<ScoreInfo> getAll();
        List<ScoreInfo> getMeritsAllTime();
        List<ScoreInfo> getMeritsThisYear();
        List<ScoreInfo> getMeritsLastWeek();
        List<ScoreInfo> getMeritsOfWeek(DateTime time);
        List<ScoreInfo> getMeritsOfMonth(DateTime time);
        List<ScoreInfo> getMeritsOfYear(DateTime time);
        List<ScoreInfo> getBadgesAllTime();
        List<ScoreInfo> getBadgesThisYear();
        List<ScoreInfo> getBadgesLastWeek();
    }
}
