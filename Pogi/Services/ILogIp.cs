using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ILogIp
    {
        void logIp(String IpAddr, string UserName);

        string getUser(String IpAddr); 
    }
}
