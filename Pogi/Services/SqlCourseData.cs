using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlCourseData : ICourseData
    {
        private PogiDbContext _context;

        public SqlCourseData(PogiDbContext context)
        {
            _context = context;
        }
        public Course add(Course course)
        {

            _context.Course.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course delete(Course course)
        {
            _context.Course.Remove(course);
            return course;
        }

        public Course get(int courseId)
        {
            return _context.Course.FirstOrDefault(r => r.CourseId == courseId);
        }

        public IEnumerable<Course> getAll()
        {
            return _context.Course.OrderBy(r => r.CourseName);
        }

        public Course update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
