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
    }
}
