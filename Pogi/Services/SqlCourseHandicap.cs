using Pogi.Data;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public class SqlCourseHandicap : ICourseHandicap
    {
        private Data.PogiDbContext _context;

        public SqlCourseHandicap(PogiDbContext context)
        {
            _context = context;
        }

        public CourseHandicap get(int courseId)
        {
            if (courseId > 0)
            {
                var CourseHandicap = _context.CourseHandicap.FirstOrDefault(r => r.CourseId == courseId);
                return CourseHandicap;
            }

            return null;
        }
    }
}
