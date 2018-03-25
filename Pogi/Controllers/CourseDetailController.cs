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
        private ICourseData _courseData;

        public CourseDetailController(ICourseDetail courseDetail, ICourseData courseData)
        {
            _courseDetail = courseDetail;
            _courseData = courseData;
        }
        [HttpGet]
        [Route("{id:int}/colors")]
        public List<SelectListItem> GetColors(int id)
        {
            var colors = _courseDetail.getColors(id);
            return colors;
        }
        [HttpGet]
        [Route("{id:int}/pars")]
        public List<string> GetPars(int id)
        {
            var CourseData = _courseData.get(id);
            List<string> Pars = new List<string>();
            Pars.Add((CourseData.Par01).ToString("00"));
            Pars.Add((CourseData.Par02).ToString("00"));
            Pars.Add((CourseData.Par03).ToString("00"));
            Pars.Add((CourseData.Par04).ToString("00"));
            Pars.Add((CourseData.Par05).ToString("00"));
            Pars.Add((CourseData.Par06).ToString("00"));
            Pars.Add((CourseData.Par07).ToString("00"));
            Pars.Add((CourseData.Par08).ToString("00"));
            Pars.Add((CourseData.Par09).ToString("00"));
            Pars.Add((CourseData.Par10).ToString("00"));
            Pars.Add((CourseData.Par11).ToString("00"));
            Pars.Add((CourseData.Par12).ToString("00"));
            Pars.Add((CourseData.Par13).ToString("00"));
            Pars.Add((CourseData.Par14).ToString("00"));
            Pars.Add((CourseData.Par15).ToString("00"));
            Pars.Add((CourseData.Par16).ToString("00"));
            Pars.Add((CourseData.Par17).ToString("00"));
            Pars.Add((CourseData.Par18).ToString("00"));

            return Pars;
        }

    }
}