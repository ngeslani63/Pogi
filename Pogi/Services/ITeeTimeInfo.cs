using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;

namespace Pogi.Services
{
    public interface ITeeTimeInfo
    {
        IEnumerable<TeeTimeInfo> getAll();
        TeeTimeInfo getTeeTimeInfo(TeeTime teeTime);

        bool majorTourDay(DateTime dateTime);
        TeeTime GetTeeTime(DateTime dateTime);
        TeeTime GetMajorTeeTime(DateTime dateTime);
        int getLockWithdrawDays(DateTime dateTime);

    }
}
