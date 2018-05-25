using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pogi.Models;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class GalleryController : Controller
    {
        private IMemberData _memberData;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IActivity _activity;

        public GalleryController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IActivity activity,
            IMemberData sqlMemberData)
        {
            _memberData = sqlMemberData;
            _userManager = userManager;
            _signInManager = signInManager;
            _activity = activity;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Swings()
        {
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                Pogi.Entities.Member Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (Member != null) userName = Member.EmailAddr1st;
            }
            _activity.logActivity(userName, "Gallery of Swings");
            ViewData["Message"] = "Swings";

            return View();
        }
        public IActionResult Members()
        {
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                Pogi.Entities.Member Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (Member != null) userName = Member.EmailAddr1st;
            }
            _activity.logActivity(userName, "Gallery of Members");
            ViewData["Message"] = "Members";
            var model = _memberData.getActive();

            return View(model);
        }
    }
}