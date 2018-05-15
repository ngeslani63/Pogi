using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Models.TeeTimeViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class TeeTimeController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ITeeTimeInfo _teeTimeInfo;
        private readonly IPlayerInfo _playerInfo;
        private readonly ITeeAssignInfo _teeAssignInfo;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberData _memberData;
        private readonly ICourseData _courseData;
        private readonly IEmailSender _emailSender;
        private readonly IActivity _activity;

        public TeeTimeController(ITeeTimeInfo teeTimeInfo, IPlayerInfo playerInfo, ITeeAssignInfo teeAssignInfo, PogiDbContext context
            , SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager, IMemberData memberData, ICourseData courseData,
              IEmailSender emailSender,
              IActivity activity)
        {
            _context = context;
            _teeTimeInfo = teeTimeInfo;
            _playerInfo = playerInfo;
            _teeAssignInfo = teeAssignInfo;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberData = memberData;
            _courseData = courseData;
            _emailSender = emailSender;
            _activity = activity;
        }

        // GET: TeeTime
        //public async Task<IActionResult> Index()
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new TeeTimeViewModel();
            model.TeeTimeInfos = _teeTimeInfo.getAll();
            model.PlayerInfos = _playerInfo.getRoster();
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "TeeTime Index");

            //model.TeeTimeInfos = _teeAssignInfo.getForTeeTime()

            return View(model);
            //return View(await _context.TeeTime.ToListAsync());
        }

        // GET: TeeTime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime
                .SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }

            return View(teeTime);
        }

        // GET: TeeTime/Create
        public IActionResult Create()
        {
            var model = new TeeTimeCreateViewModel();
            model.Courses = _courseData.getSelectList();
            if (_signInManager.IsSignedIn(User))
            {
                model.Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                model.ReservedById = model.Member.MemberId;
            }
            return View(model);
        }

        // POST: TeeTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TeeTimeId,ReservedById,TeeTimeTS,CourseId,NumPlayers")] TeeTime teeTime)
        public async Task<IActionResult> Create(TeeTimeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime ts;
                if (!DateTime.TryParse(model.TeeTimeTS.ToString(), out ts))
                {
                    ModelState.AddModelError("TeeTimeTS", "Invalid Date");
                    model.Courses = _courseData.getSelectList();
                    model.Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    model.ReservedById = model.Member.MemberId;
                    return View(model);
                }
                if (ts < DateTime.Now)
                {
                    ModelState.AddModelError("TeeTimeTS", "Please specify a future Date and Time");
                    model.Courses = _courseData.getSelectList();
                    model.Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    model.ReservedById = model.Member.MemberId;
                    return View(model);
                }
                TeeTime teeTime = new TeeTime();
                teeTime.TeeTimeId = model.TeeTimeId;
                teeTime.ReservedById = model.ReservedById;
                teeTime.TeeTimeTS = ts;
                teeTime.CourseId = model.CourseId;
                teeTime.NumPlayers = model.NumPlayers;
                teeTime.MajorTour = model.MajorTour;
                teeTime.TeeTimeInterval = model.TeeTimeInterval;
                teeTime.AutoAssign = false;
                _context.Add(teeTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Courses = _courseData.getSelectList();
            model.Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            model.ReservedById = model.Member.MemberId;
            return View(model);
        }

        // GET: TeeTime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime.SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }
            var model = new TeeTimeCreateViewModel();
            model.Courses = _courseData.getSelectList();
            model.TeeTimeId = teeTime.TeeTimeId;
            model.ReservedById = teeTime.ReservedById;
            model.TeeTimeTS = teeTime.TeeTimeTS;
            model.CourseId = teeTime.CourseId;
            model.NumPlayers = teeTime.NumPlayers;
            model.MajorTour = teeTime.MajorTour;
            model.TeeTimeInterval = teeTime.TeeTimeInterval;

            model.Member = _memberData.get(teeTime.ReservedById);


            return View(model);
        }

        // POST: TeeTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeeTimeCreateViewModel model)
        {
            if (id != model.TeeTimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime ts;
                    if (!DateTime.TryParse(model.TeeTimeTS.ToString(), out ts))
                    {
                        ModelState.AddModelError("TeeTimeTS", "Invalid Date");
                        model.Courses = _courseData.getSelectList();
                        model.Member = _memberData.get(model.ReservedById) ;
;
                        return View(model);
                    }
                    if (ts < DateTime.Now)
                    {
                        ModelState.AddModelError("TeeTimeTS", "Please specify a future Date and Time");
                        model.Courses = _courseData.getSelectList();
                        model.Member = _memberData.get(model.ReservedById);
                        return View(model);
                    }
                    TeeTime teeTime = new TeeTime();
                    teeTime.TeeTimeId = model.TeeTimeId;
                    teeTime.ReservedById = model.ReservedById;
                    teeTime.TeeTimeTS = ts;
                    teeTime.CourseId = model.CourseId;
                    teeTime.NumPlayers = model.NumPlayers;
                    teeTime.MajorTour = model.MajorTour;
                    teeTime.TeeTimeInterval = model.TeeTimeInterval;
                    teeTime.AutoAssign = false;
                       
                    _context.Update(teeTime);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeeTimeExists(model.TeeTimeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            model.Courses = _courseData.getSelectList();
            model.Member = _memberData.get(model.ReservedById);
            return View(model);
        }

        // GET: TeeTime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime
                .SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }

            var model = new TeeTimeDisplayViewModel();
            model.TeeTimeId = teeTime.TeeTimeId;
            model.TeeTimeTS = teeTime.TeeTimeTS;
            model.CourseName = _courseData.get(teeTime.CourseId).CourseName;
            model.NumPlayers = teeTime.NumPlayers;
            model.Member = _memberData.get(teeTime.ReservedById);
            
            return View(model);
        }

        // POST: TeeTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teeTime = await _context.TeeTime.SingleOrDefaultAsync(m => m.TeeTimeId == id);
            _context.TeeTime.Remove(teeTime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeTimeExists(int id)
        {
            return _context.TeeTime.Any(e => e.TeeTimeId == id);
        }
    }
}
