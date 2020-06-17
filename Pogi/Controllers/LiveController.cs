using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pogi.Controllers
{
    public class LiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
