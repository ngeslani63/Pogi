using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<SelectListItem> getSelectList()
        {
            return getSelectList(0);
        }
        public List<SelectListItem> getSelectList(int courseId)
        {
            List<SelectListItem> courseList = new List<SelectListItem>();
            IEnumerable<Course> courses = _context.Course.OrderBy(r => r.CourseName);
            foreach (Course course in courses)
            {
                SelectListItem sl = new SelectListItem { Text = course.CourseName, Value = course.CourseId.ToString() };
                if (courseId > 0 && courseId == course.CourseId) sl.Selected = true;
                courseList.Add(sl);
            }
            return courseList;
        }

        public Course update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
