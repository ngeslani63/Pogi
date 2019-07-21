using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;

namespace Pogi.Services
{
    public interface ITeeTimeInfo
    {
        IEnumerable<TeeTimeInfo> getAll();
        bool majorTourDay(DateTime dateTime);
        TeeTime GetTeeTime(DateTime dateTime);
        int getLockWithdrawDays(DateTime dateTime);

    }
}
