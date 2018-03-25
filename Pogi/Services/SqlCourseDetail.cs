using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlCourseDetail : ICourseDetail
    {
        private PogiDbContext _context;

        public SqlCourseDetail(PogiDbContext context)
        {
            _context = context;
        }
        public CourseDetail add(CourseDetail CourseDetail)
        {
            _context.CourseDetail.Add(CourseDetail);
            _context.SaveChanges();
            return CourseDetail;
        }

        public CourseDetail delete(CourseDetail CourseDetail)
        {
            _context.CourseDetail.Remove(CourseDetail);
            return CourseDetail;
        }

        public void deleteAll(int CourseId)
        {
            _context.CourseDetail.Where(r => r.CourseId == CourseId).ToList().ForEach(p => _context.CourseDetail.Remove(p));
            return;
        }

        public CourseDetail get(int CourseId, string Color)
        {
            return _context.CourseDetail.FirstOrDefault(r => r.CourseId == CourseId && r.Color == Color  );
        }

        public List<CourseDetail> getAll(int CourseId)
        {
            var CourseDetails =  _context.CourseDetail.Where(r => r.CourseId == CourseId).OrderBy(r => r.Slope);
            var courseList = new List<CourseDetail>();
            foreach (var item in CourseDetails)
            {
                courseList.Add(item);
            }
            return courseList;
        }

        public List<SelectListItem> getSelectList(int CourseId)
        {
            //throw new NotImplementedException();
            List<SelectListItem> courseDetailList = new List<SelectListItem>();
            IEnumerable<CourseDetail> courseDetails = _context.CourseDetail.Where(r => r.CourseId == CourseId).OrderBy(r => r.Slope);
            foreach (CourseDetail courseDetail in courseDetails)
            {
                var selected = false;
                if (courseDetail.Color.Equals("white")) selected = true;
                SelectListItem sl = new SelectListItem { Text = courseDetail.Color, Value = courseDetail.Color, Selected = selected };
                courseDetailList.Add(sl);
            }
            return courseDetailList;

        }

        public CourseDetail update(CourseDetail CourseDetail)
        {
            _context.CourseDetail.Update(CourseDetail);
            return CourseDetail;
        }
    }
}
