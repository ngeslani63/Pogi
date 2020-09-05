using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ICourseHandicap
    {
        CourseHandicap get(int courseId);
    }
}
