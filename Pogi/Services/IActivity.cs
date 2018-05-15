using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IActivity
    {
        void logActivity(string userName, string Action);
    }
}
