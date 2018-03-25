using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Produces("application/json")]
    [Route("api/CourseDetail")]
    public class CourseDetailController : Controller
    {
        private ICourseDetail _courseDetail;

        public CourseDetailController(ICourseDetail courseDetail)
        {
            _courseDetail = courseDetail;
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public List<SelectListItem> Get(int id)
        {
            var colors = _courseDetail.getSelectList(id);
            return colors;
        }

    }
}