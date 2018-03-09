using Pogi.Entities;
using Pogi.Models.TeeTimeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
