using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class GalleryController : Controller
    {
        private IMemberData _memberData;

        public GalleryController(IMemberData sqlMemberData)
        {
            _memberData = sqlMemberData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Swings()
        {
            ViewData["Message"] = "Swings";

            return View();
        }
        public IActionResult Members()
        {
            ViewData["Message"] = "Members";
            var model = _memberData.getActive();

            return View(model);
        }
    }
}