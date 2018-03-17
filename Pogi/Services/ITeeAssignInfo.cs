using Pogi.Models;
using System.Collections.Generic;

namespace Pogi.Services
{
    public interface ITeeAssignInfo
    {
        IEnumerable<TeeAssignInfo> getAll();

        List<TeeAssignInfo> getForTeeTime(int teeTimeId);
    }
}
