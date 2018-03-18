using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.CourseViewModels
{
    public class CourseEditViewModel
    {
        public CourseEditViewModel()
        {
    
        }
        public Course Course { get; set; }

         public List<CourseDetail> CourseDetails { get; set; }
    }
}
