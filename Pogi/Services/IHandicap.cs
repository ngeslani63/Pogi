using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IHandicap
    {

        IEnumerable<Handicap> getForMemberId(int MemberId);
        IEnumerable<Handicap> getForGhin(int GhinNumber);

        DateTime getNextDate(int GhinNumber);
        DateTime getCurrEffDate();
        DateTime getNextEffDate();
        List<SelectListItem> getActiveDates(string Date);
        DateTime getRevBeginDate(DateTime EffDate);

        Handicap getHandicapForDate(int GhinNumber, DateTime Date);

        IEnumerable<HandicapInfo> getAllForDate(DateTime Date);

        int getS36Hcp(Score Score, Course Course);



    }
}
