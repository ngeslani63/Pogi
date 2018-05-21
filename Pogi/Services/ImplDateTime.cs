using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public class ImplDateTime : IDateTime
    {
        public ImplDateTime()
        {
        }
        TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public DateTime getNow()
        {
            DateTime Now = (TimeZoneInfo.ConvertTime(DateTime.Now, est));
            return Now;
        }

        public DateTime getToday()
        {
            DateTime Today = (TimeZoneInfo.ConvertTime(DateTime.Now, est)).Date;
            return Today;
        }
    }
}
