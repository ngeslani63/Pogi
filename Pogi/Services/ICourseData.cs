using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    interface ICourseData
    {
        IEnumerable<Course> getAll();
        Course get(int courseId);
        Course delete(Course course);
        Course add(Course course);
        Course update(Course course);
    }
    
}
