using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface ICourseData
    {
        IEnumerable<Course> getAll();
        List<SelectListItem> getSelectList();
        Course get(int courseId);
        Course delete(Course course);
        Course add(Course course);
        Course update(Course course);
    }
    
}
