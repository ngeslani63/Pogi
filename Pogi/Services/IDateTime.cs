﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IDateTime
    {
        DateTime getNow();
        DateTime getToday();
    }
}
