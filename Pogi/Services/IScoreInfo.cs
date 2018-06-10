using Pogi.Entities;
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
        List<ScoreInfo> getForTour(string TourName);
        List<ScoreInfo> getForTour(int TourId);
        List<ScoreInfo> getPodiumForTour(int TourId);
        List<ScoreInfo> getMeritsAllTime();
        List<ScoreInfo> getMeritsThisYear();
        List<ScoreInfo> getMeritsLastWeek();
        List<ScoreInfo> getMeritsOfWeek(DateTime time);
        List<ScoreInfo> getMeritsOfMonth(DateTime time);
        List<ScoreInfo> getMeritsOfYear(DateTime time);
        List<ScoreInfo> getBadgesAllTime();
        List<ScoreInfo> getBadgesThisYear();
        List<ScoreInfo> getBadgesLastWeek();
        List<ScoreInfo> getBadgesOfWeek(DateTime time);
        List<ScoreInfo> getBadgesOfMonth(DateTime time);
        List<ScoreInfo> getBadgesOfYear(DateTime time);

        String getTiebreaker(Score Score);
    }
}
