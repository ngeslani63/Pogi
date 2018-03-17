using Pogi.Models;
using System.Collections.Generic;

namespace Pogi.Services
{
    public interface ITeeTimeInfo
    {
            IEnumerable<TeeTimeInfo> getAll();
            //TeeTime get(int teeTimeId);
            //TeeTime delete(TeeTime course);
            //TeeTime add(TeeTime course);
            //TeeTime update(TeeTime course);
   
    }
}
