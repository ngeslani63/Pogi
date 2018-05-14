using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ICourseDetail
    {
        List<CourseDetail> getAll(int CourseId);
        List<SelectListItem> getColors(int CourseId);
        CourseDetail get(int courseId, string Color);
        CourseDetail delete(CourseDetail CourseDetail);
        void deleteAll(int CourseId);
        CourseDetail add(CourseDetail CourseDetail);
        CourseDetail update(CourseDetail CourseDetail);
       
    }
}
