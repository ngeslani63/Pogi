using Pogi.Data;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public class SqlCourseMap : ICourseMap
    {
        private PogiDbContext _context;
        public SqlCourseMap(PogiDbContext context)
        {
            _context = context;
        }
        public CourseMap get(int courseId)
        {
            return _context.CourseMap.FirstOrDefault(r => r.CourseId == courseId);
        }
    }
}
