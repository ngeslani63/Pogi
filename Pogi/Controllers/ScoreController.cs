using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Models.ScoreViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class ScoreController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IScoreInfo _scoreInfo;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberData _memberData;
        private readonly ICourseData _courseData;
        private readonly ICourseDetail _courseDetail;
        private readonly IHandicap _handicap;
        private readonly ITourInfo _tourInfo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActivity _activity;
        private readonly IDateTime _dateTime;

        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ScoreController(PogiDbContext context, IScoreInfo scoreInfo,
             SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager, IMemberData memberData, ICourseData courseData,
                ICourseDetail courseDetail,
                IHandicap handicap,
                ITourInfo tourInfo,
                IHttpContextAccessor httpContextAccessor,
              IActivity activity,
              IDateTime dateTime)
        {
            _context = context;
            _scoreInfo = scoreInfo;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberData = memberData;
            _courseData = courseData;
            _courseDetail = courseDetail;
            _handicap = handicap;
            _tourInfo = tourInfo;
            _httpContextAccessor = httpContextAccessor;
            _activity = activity;
            _dateTime = dateTime;
        }

        // GET: Score
        [AllowAnonymous]
        //public async Task<IActionResult> Index()
        public IActionResult Index(string Search, string TourId)
        {
            if (Search == null || Search.Length == 0)
            {
                Search = _session.GetString("SearchLogbook");
                if (Search == null || Search.Length == 0)
                {
                    _session.Remove("SearchLogbook");
                    Search = "";
                }
            }
            else
            {
                if (Search.Equals("***"))
                {
                    _session.Remove("SearchLogbook");
                    Search = "";
                }
                else
                {
                    _session.SetString("SearchLogbook", Search);
                }
            }
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLogbook");
                if (TourId == null || TourId.Length == 0)
                {
                    _session.Remove("TourIdLogbook");
                    TourId = "0";
                }
            }
            else
            {
                _session.SetString("TourIdLogbook", TourId);
            }
            var model = new ScoreDisplayViewModel();
            model.Search = Search;
            if (TourId.Length > 0 && int.Parse(TourId) > 0)
            {
                model.ScoreInfos = _scoreInfo.getForTour(int.Parse(TourId));
                model.TourId = TourId;
            }
            else
            {
                model.ScoreInfos = _scoreInfo.getAll();
                model.TourId = "0";
            }
            model.Tours = _tourInfo.getTours(true);
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Logbook");
            return View(model);
            //return View(await _context.Score.ToListAsync());
        }
        [AllowAnonymous]
        public IActionResult Leaderboard(string TourId)
        {
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLeaderboard");
                if (TourId == null || TourId.Length == 0)
                {
                    Tour tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                    }
                    else
                    {
                        TourId = "1";
                    }
                    _session.SetString("TourIdLeaderboard", TourId);
                }
            }
            else
            {
                _session.SetString("TourIdLeaderboard", TourId);
            }
            var model = new ScoreDisplayViewModel();
            if (TourId.Length > 0 && int.Parse(TourId) > 0)
            {
                model.ScoreInfos = _scoreInfo.getForTour(int.Parse(TourId));
                model.TourId = TourId;
            }
            model.Tours = _tourInfo.getTours(false);
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Leaderboard");
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Podium(string TourId)
        {

            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdPodium");
                if (TourId == null || TourId.Length == 0)
                {
                    Tour tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                    }
                    else
                    {
                        TourId = "1";
                    }
                    _session.SetString("TourIdPodium", TourId);
                }
            }
            else
            {
                _session.SetString("TourIdPodium", TourId);
            }
            var model = new ScorePodiumViewModel();
            if (TourId.Length > 0 && int.Parse(TourId) > 0)
            {
                model.ScoreInfos = _scoreInfo.getPodiumForTour(int.Parse(TourId));
                model.TourId = TourId;
            }
            else
            {
                model.ScoreInfos = null;
                model.TourId = "0";
            }
            model.Tours = _tourInfo.getTours(false);
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Podium");
            return View(model);
            //return View(await _context.Score.ToListAsync());
        }

        [AllowAnonymous]
        //public async Task<IActionResult> Performers()
        public IActionResult Performers()
        {
            string userName = "";
            Member Member = null;
            if (_signInManager.IsSignedIn(User))
            {
                Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (Member != null) userName = Member.EmailAddr1st;
            }
            _activity.logActivity(userName, "Performers");

            var PerformersWeekly = _session.GetString("PerformersWeekly");
            var PerformersMonthly = _session.GetString("PerformersMonthly");
            var PerformersYearly = _session.GetString("PerformersYearly");
            var PerformersAllTime = _session.GetString("PerformersAllTime");
            var PerformersAsOfDate = _session.GetString("PerformersAsOfDate");
            var model = new ScorePerformersViewModel();

            if (PerformersAsOfDate != null && PerformersAsOfDate.Length > 0)
            {
                model.AsOfDate = DateTime.ParseExact(PerformersAsOfDate, "M/d/yyyy", CultureInfo.CurrentCulture);
            }
            else
            {
                DateTime today = DateTime.Today;
                int daysSinceSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek - 7) % 7;
                model.AsOfDate = today.AddDays(daysSinceSunday); // Sunday of Last Week
            }

            if (PerformersWeekly == "Y") model.Weekly = true;
            if (PerformersMonthly == "Y") model.Monthly = true;
            if (PerformersYearly == "Y") model.Yearly = true;
            if (PerformersAllTime == "Y") model.AllTime = true;
            if (!(model.Weekly || model.Monthly || model.Yearly || model.AllTime))
            {
                model.Weekly = true;
                model.AllTime = true;
                _session.SetString("PerformersWeekly", "Y");
                _session.SetString("PerformersAllTime", "Y");
            }
            model.ScoreInfos = new List<ScoreInfo>();
            if (model.Weekly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfWeek(model.AsOfDate));
            if (model.Monthly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfMonth(model.AsOfDate));
            if (model.Yearly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfYear(model.AsOfDate));
            if (model.AllTime) model.ScoreInfos.AddRange(_scoreInfo.getMeritsAllTime());
            //model = _scoreInfo.getMeritsAllTime();
            _session.SetString("PerformersAsOfdate", model.AsOfDate.ToShortDateString());

            return View(model);
            //return View(await _context.Score.ToListAsync());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Performers()
        //public IActionResult Performers(string AsOfDate, bool Weekly, bool Monthly, bool Yearly, bool Alltime )
        public IActionResult Performers(ScorePerformersViewModel model)
        {
            //var model = new ScorePerformersViewModel();
            if (!(model.Weekly || model.Monthly || model.Yearly || model.AllTime))
            {
                //model.Weekly = true;
                //model.AllTime = true;
                //_session.SetString("PerformersWeekly", "Y");
                //_session.SetString("PerformersAllTime", "Y");
            }
            model.ScoreInfos = new List<ScoreInfo>();
            //if (AsOfDate != null && AsOfDate.Length > 0)
            //{
            //    model.AsOfDate = DateTime.ParseExact(AsOfDate, "M/d/yyyy", CultureInfo.CurrentCulture);
            //}
            //else
            //{
            //    model.AsOfDate = DateTime.Today;
            //}
            //model.Weekly = Weekly;
            //model.Monthly = Monthly;
            //model.Yearly = Yearly;
            //model.AllTime = Alltime;
            if (model.Weekly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfWeek(model.AsOfDate));
            if (model.Monthly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfMonth(model.AsOfDate));
            if (model.Yearly) model.ScoreInfos.AddRange(_scoreInfo.getMeritsOfYear(model.AsOfDate));
            if (model.AllTime) model.ScoreInfos.AddRange(_scoreInfo.getMeritsAllTime());
            if (model.Weekly) _session.SetString("PerformersWeekly", "Y");
            else _session.SetString("PerformersWeekly", "N");
            if (model.Monthly) _session.SetString("PerformersMonthly", "Y");
            else _session.SetString("PerformersMonthly", "N");
            if (model.Yearly) _session.SetString("PerformersYearly", "Y");
            else _session.SetString("PerformersYearly", "N");
            if (model.AllTime) _session.SetString("PerformersAllTime", "Y");
            else _session.SetString("PerformersAllTime", "N");
            _session.SetString("PerformersAsOfDate", model.AsOfDate.ToShortDateString());


            return View(model);

        }
        //[AllowAnonymous]
        //public IActionResult Badges()
        //{
        //    var model = new List<ScoreInfo>();

        //    model = _scoreInfo.getBadgesLastWeek();
        //    model.AddRange(_scoreInfo.getBadgesAllTime());
        //    //model = _scoreInfo.getMeritsAllTime();

        //    return View(model);
        //    //return View(await _context.Score.ToListAsync());
        //}
        [AllowAnonymous]
        //public async Task<IActionResult> Performers()
        public IActionResult Badges()
        {
            string userName = "";
            Member Member = null;
            if (_signInManager.IsSignedIn(User))
            {
                Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (Member != null) userName = Member.EmailAddr1st;
            }
            _activity.logActivity(userName, "Badges");

            var BadgesWeekly = _session.GetString("BadgesWeekly");
            var BadgesMonthly = _session.GetString("BadgesMonthly");
            var BadgesYearly = _session.GetString("BadgesYearly");
            var BadgesAllTime = _session.GetString("BadgesAllTime");
            var BadgesAsOfDate = _session.GetString("BadgesAsOfDate");
            var model = new ScorePerformersViewModel();

            if (BadgesAsOfDate != null && BadgesAsOfDate.Length > 0)
            {
                model.AsOfDate = DateTime.ParseExact(BadgesAsOfDate, "M/d/yyyy", CultureInfo.CurrentCulture);
            }
            else
            {
                DateTime today = DateTime.Today;
                int daysSinceSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek - 7) % 7;
                model.AsOfDate = today.AddDays(daysSinceSunday); // Sunday of Last Week
            }

            if (BadgesWeekly == "Y") model.Weekly = true;
            if (BadgesMonthly == "Y") model.Monthly = true;
            if (BadgesYearly == "Y") model.Yearly = true;
            if (BadgesAllTime == "Y") model.AllTime = true;
            if (!(model.Weekly || model.Monthly || model.Yearly || model.AllTime))
            {
                model.Weekly = true;
                model.AllTime = true;
                _session.SetString("BadgesWeekly", "Y");
                _session.SetString("BadgesAllTime", "Y");
            }
            model.ScoreInfos = new List<ScoreInfo>();
            if (model.Weekly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfWeek(model.AsOfDate));
            if (model.Monthly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfMonth(model.AsOfDate));
            if (model.Yearly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfYear(model.AsOfDate));
            if (model.AllTime) model.ScoreInfos.AddRange(_scoreInfo.getBadgesAllTime());
            //model = _scoreInfo.getBadgesAllTime();
            _session.SetString("BadgesAsOfdate", model.AsOfDate.ToShortDateString());

            return View(model);
            //return View(await _context.Score.ToListAsync());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Badges()
        //public IActionResult Badges(string AsOfDate, bool Weekly, bool Monthly, bool Yearly, bool Alltime )
        public IActionResult Badges(ScorePerformersViewModel model)
        {
            //var model = new ScoreBadgesViewModel();
            if (!(model.Weekly || model.Monthly || model.Yearly || model.AllTime))
            {
                //model.Weekly = true;
                //model.AllTime = true;
                //_session.SetString("BadgesWeekly", "Y");
                //_session.SetString("BadgesAllTime", "Y");
            }
            model.ScoreInfos = new List<ScoreInfo>();
            //if (AsOfDate != null && AsOfDate.Length > 0)
            //{
            //    model.AsOfDate = DateTime.ParseExact(AsOfDate, "M/d/yyyy", CultureInfo.CurrentCulture);
            //}
            //else
            //{
            //    model.AsOfDate = DateTime.Today;
            //}
            //model.Weekly = Weekly;
            //model.Monthly = Monthly;
            //model.Yearly = Yearly;
            //model.AllTime = Alltime;
            if (model.Weekly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfWeek(model.AsOfDate));
            if (model.Monthly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfMonth(model.AsOfDate));
            if (model.Yearly) model.ScoreInfos.AddRange(_scoreInfo.getBadgesOfYear(model.AsOfDate));
            if (model.AllTime) model.ScoreInfos.AddRange(_scoreInfo.getBadgesAllTime());
            if (model.Weekly) _session.SetString("BadgesWeekly", "Y");
            else _session.SetString("BadgesWeekly", "N");
            if (model.Monthly) _session.SetString("BadgesMonthly", "Y");
            else _session.SetString("BadgesMonthly", "N");
            if (model.Yearly) _session.SetString("BadgesYearly", "Y");
            else _session.SetString("BadgesYearly", "N");
            if (model.AllTime) _session.SetString("BadgesAllTime", "Y");
            else _session.SetString("BadgesAllTime", "N");
            _session.SetString("BadgesAsOfDate", model.AsOfDate.ToShortDateString());


            return View(model);

        }


        // GET: Score/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Score/Create
        public IActionResult Create()
        {
            //var model = new Score();
            //model.CreatedBy = User.Identity.Name;
            //model.CreatedTs = DateTime.Now;
            //model.LastUpdatedBy = User.Identity.Name;
            //model.LastUpdatedTs = DateTime.Now;
            //return View(model);

            var Color = _session.GetString("Color");
            var CourseId = _session.GetString("CourseId");
            var ScoreDate = _session.GetString("ScoreDate");
            var TourEvent = _session.GetString("TourEvent");
            var TourId = _session.GetString("TourId");

            var model = new ScoreCreateViewModel();
            model.Members = _memberData.getSelectList();
            model.Tours = _tourInfo.getTours();

            if (CourseId != null && int.Parse(CourseId) > 0 && Color != null && Color.Length > 0)
            {
                model.Courses = _courseData.getSelectList(int.Parse(CourseId));
                model.Colors = _courseDetail.getColors(int.Parse(CourseId));
                model.ScoreDate = DateTime.Parse(ScoreDate, CultureInfo.CurrentCulture);
                model.CourseId = int.Parse(CourseId);
                model.Color = Color;
            }
            else
            {
                model.Courses = _courseData.getSelectList();
                model.Colors = _courseDetail.getColors(Int32.Parse(model.Courses[0].Value));
            }
            if (TourEvent != null && TourEvent == "Y")
            {
                model.TourEvent = true;
                if (TourId != null)
                {
                    model.TourId = int.Parse(TourId);
                }
            }



            if (_signInManager.IsSignedIn(User))
            {
                model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            }
            return View(model);
        }


        // POST: Score/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        //public async Task<IActionResult> Create([Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        public async Task<IActionResult> Create(ScoreCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime ts;
                if (!DateTime.TryParse(model.ScoreDate.ToString(), out ts))
                {
                    ModelState.AddModelError("ScoreDate", "Invalid Date");
                    model.Courses = _courseData.getSelectList();
                    model.Members = _memberData.getSelectList();
                    model.Colors = _courseDetail.getColors(Int32.Parse(model.Courses[0].Value));
                    model.Tours = _tourInfo.getTours();
                    model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    return View(model);
                }
                if (ts > DateTime.Now)
                {
                    ModelState.AddModelError("ScoreDate", "Please do not specify a future Date and Time");
                    model.Courses = _courseData.getSelectList();
                    model.Members = _memberData.getSelectList();
                    model.Colors = _courseDetail.getColors(Int32.Parse(model.Courses[0].Value));
                    model.Tours = _tourInfo.getTours();
                    model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    return View(model);
                }
                //_context.Add(score);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                Score Score = new Score();
                Score.MemberId = model.MemberId;
                Score.CourseId = model.CourseId;
                Score.Color = model.Color;
                Score.ScoreDate = model.ScoreDate;
                Score.EnteredById = model.EnteredBy.MemberId;
                Score.Hole01 = model.Hole01;
                Score.Hole02 = model.Hole02;
                Score.Hole03 = model.Hole03;
                Score.Hole04 = model.Hole04;
                Score.Hole05 = model.Hole05;
                Score.Hole06 = model.Hole06;
                Score.Hole07 = model.Hole07;
                Score.Hole08 = model.Hole08;
                Score.Hole09 = model.Hole09;
                Score.Hole10 = model.Hole10;
                Score.Hole11 = model.Hole11;
                Score.Hole12 = model.Hole12;
                Score.Hole13 = model.Hole13;
                Score.Hole14 = model.Hole14;
                Score.Hole15 = model.Hole15;
                Score.Hole16 = model.Hole16;
                Score.Hole17 = model.Hole17;
                Score.Hole18 = model.Hole18;
                Score.HoleIn = model.HoleIn;
                Score.HoleOut = model.HoleOut;
                Score.HoleTotal = model.HoleTotal;
                Score.TourEvent = model.TourEvent;
                Score.TourId = model.TourId;
                if (model.AboutGame == null) model.AboutGame = "";
                Score.AboutGame = model.AboutGame;

                Score.NetScore = 199;
                Score.TourScore = 199;
                float HcpAllowPct = 100.0F;
                Tour Tour;
                if (Score.TourEvent == true && (Tour = _tourInfo.getTour(Score.TourId)) != null)
                {
                    HcpAllowPct = Tour.HcpAllowPct;
                }

                Handicap Handicap = _handicap.getHandicapForDate(Score.MemberId, model.ScoreDate);
                CourseDetail CourseDetail = _courseDetail.get(model.CourseId, model.Color);
                Course Course = _courseData.get(Score.CourseId);

                if (Handicap != null && Handicap.HcpIndex > 0)
                {
                    float courseHandicap = Handicap.HcpIndex * CourseDetail.Slope / 113;
                    float courseHandicapT = Handicap.HcpIndex * (HcpAllowPct / 100) * CourseDetail.Slope / 113;
                    Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap);
                    Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT);
                }
                else
                {
                    float courseHandicap = getCallawayHcp(Score, Course) * CourseDetail.Slope / 113;
                    float courseHandicapT = getCallawayHcp(Score, Course) * (HcpAllowPct / 100) * CourseDetail.Slope / 113;
                    Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap);
                    Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT);

                }

                countScores(Score, Course);
                Score.Tiebreaker = _scoreInfo.getTiebreaker(Score);

                Score.CreatedBy = User.Identity.Name;
                Score.CreatedTs = DateTime.Now;
                Score.LastUpdatedBy = User.Identity.Name;
                Score.LastUpdatedTs = DateTime.Now;

                _context.Add(Score);
                await _context.SaveChangesAsync();
                // Save Last Selected ScoreDate, CourseId and Color
                var temp = model.ScoreDate.ToString();
                _session.SetString("ScoreDate", model.ScoreDate.ToString());
                _session.SetString("CourseId", model.CourseId.ToString());
                _session.SetString("Color", model.Color);
                if (model.TourEvent == true)
                {
                    _session.SetString("TourEvent", "Y");
                    _session.SetString("TourId", model.TourId.ToString());
                }
                else
                {
                    _session.SetString("TourEvent", "N");
                    _session.Remove("TourId");
                }

                return RedirectToAction(nameof(Index));
            }
            model.Courses = _courseData.getSelectList();
            model.Members = _memberData.getSelectList();
            model.Colors = _courseDetail.getColors(Int32.Parse(model.Courses[0].Value));
            model.Tours = _tourInfo.getTours();
            model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            return View(model);
        }

        // GET: Score/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }
            var model = new ScoreCreateViewModel();
            model.Courses = _courseData.getSelectList();
            model.Members = _memberData.getSelectList();
            model.ScoreId = score.ScoreId;
            model.MemberId = score.MemberId;
            model.CourseId = score.CourseId;
            model.Colors = _courseDetail.getColors(score.CourseId);
            model.Tours = _tourInfo.getTours();
            model.Color = score.Color;
            model.ScoreDate = score.ScoreDate;
            model.EnteredBy = _memberData.get(score.EnteredById);
            model.Hole01 = score.Hole01;
            model.Hole02 = score.Hole02;
            model.Hole03 = score.Hole03;
            model.Hole04 = score.Hole04;
            model.Hole05 = score.Hole05;
            model.Hole06 = score.Hole06;
            model.Hole07 = score.Hole07;
            model.Hole08 = score.Hole08;
            model.Hole09 = score.Hole09;
            model.Hole10 = score.Hole10;
            model.Hole11 = score.Hole11;
            model.Hole12 = score.Hole12;
            model.Hole13 = score.Hole13;
            model.Hole14 = score.Hole14;
            model.Hole15 = score.Hole15;
            model.Hole16 = score.Hole16;
            model.Hole17 = score.Hole17;
            model.Hole18 = score.Hole18;
            model.HoleIn = score.HoleIn;
            model.HoleOut = score.HoleOut;
            model.HoleTotal = score.HoleTotal;
            model.TourEvent = score.TourEvent;
            model.TourId = score.TourId;
            model.AboutGame = score.AboutGame;
            if (model.AboutGame == null) model.AboutGame = "";

            if (_signInManager.IsSignedIn(User))
            {
                model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            }
            return View(model);

        }

        // POST: Score/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        public async Task<IActionResult> Edit(int id, ScoreCreateViewModel model)
        {
            if (id != model.ScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Score Score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreId == id);
                    Score.CourseId = model.CourseId;
                    Score.Color = model.Color;
                    Score.ScoreDate = model.ScoreDate;
                    Score.EnteredById = model.EnteredBy.MemberId;
                    Score.Hole01 = model.Hole01;
                    Score.Hole02 = model.Hole02;
                    Score.Hole03 = model.Hole03;
                    Score.Hole04 = model.Hole04;
                    Score.Hole05 = model.Hole05;
                    Score.Hole06 = model.Hole06;
                    Score.Hole07 = model.Hole07;
                    Score.Hole08 = model.Hole08;
                    Score.Hole09 = model.Hole09;
                    Score.Hole10 = model.Hole10;
                    Score.Hole11 = model.Hole11;
                    Score.Hole12 = model.Hole12;
                    Score.Hole13 = model.Hole13;
                    Score.Hole14 = model.Hole14;
                    Score.Hole15 = model.Hole15;
                    Score.Hole16 = model.Hole16;
                    Score.Hole17 = model.Hole17;
                    Score.Hole18 = model.Hole18;
                    Score.HoleIn = model.HoleIn;
                    Score.HoleOut = model.HoleOut;
                    Score.HoleTotal = model.HoleTotal;
                    Score.TourEvent = model.TourEvent;
                    Score.TourId = model.TourId;
                    if (model.AboutGame == null) model.AboutGame = "";
                    Score.AboutGame = model.AboutGame;

                    Score.NetScore = 199;
                    Score.TourScore = 199;
                    float HcpAllowPct = 100.0F;
                    Tour Tour;
                    if (Score.TourEvent == true && (Tour = _tourInfo.getTour(Score.TourId)) != null)
                    {
                        HcpAllowPct = Tour.HcpAllowPct;
                    }

                    Handicap Handicap = _handicap.getHandicapForDate(Score.MemberId, model.ScoreDate);
                    CourseDetail CourseDetail = _courseDetail.get(model.CourseId, model.Color);
                    Course Course = _courseData.get(Score.CourseId);

                    if (Handicap != null && Handicap.HcpIndex > 0)
                    {
                        float courseHandicap = Handicap.HcpIndex * CourseDetail.Slope / 113;
                        float courseHandicapT = Handicap.HcpIndex * (HcpAllowPct / 100) * CourseDetail.Slope / 113;
                        Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap);
                        Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT);
                    }
                    else
                    {
                        float courseHandicap = getCallawayHcp(Score, Course) * CourseDetail.Slope / 113;
                        float courseHandicapT = getCallawayHcp(Score, Course) * (HcpAllowPct / 100) * CourseDetail.Slope / 113;
                        Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap);
                        Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT);

                    }

                    countScores(Score, Course);
                    Score.Tiebreaker = _scoreInfo.getTiebreaker(Score);

                    Score.LastUpdatedBy = User.Identity.Name;
                    Score.LastUpdatedTs = DateTime.Now;


                    _context.Update(Score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(model.ScoreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Score/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Score/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreId == id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.ScoreId == id);
        }
        private void countScores(Score Score, Course Course)
        {
            Score.HoleInOnes = 0;
            Score.Albatross = 0;
            Score.Eagles = 0;
            Score.Birdies = 0;
            Score.Pars = 0;
            Score.Bogeys = 0;
            Score.DoubleBogeys = 0;
            Score.TripleBogeys = 0;
            Score.QuadBogeys = 0;
            Score.MaxScore = 0;
            Score.Disaster = 0;

            countBadges(Score, Score.Hole01, Course.Par01);
            countBadges(Score, Score.Hole02, Course.Par02);
            countBadges(Score, Score.Hole03, Course.Par03);
            countBadges(Score, Score.Hole04, Course.Par04);
            countBadges(Score, Score.Hole05, Course.Par05);
            countBadges(Score, Score.Hole06, Course.Par06);
            countBadges(Score, Score.Hole07, Course.Par07);
            countBadges(Score, Score.Hole08, Course.Par08);
            countBadges(Score, Score.Hole09, Course.Par09);
            countBadges(Score, Score.Hole10, Course.Par10);
            countBadges(Score, Score.Hole11, Course.Par11);
            countBadges(Score, Score.Hole12, Course.Par12);
            countBadges(Score, Score.Hole13, Course.Par13);
            countBadges(Score, Score.Hole14, Course.Par14);
            countBadges(Score, Score.Hole15, Course.Par15);
            countBadges(Score, Score.Hole16, Course.Par16);
            countBadges(Score, Score.Hole17, Course.Par17);
            countBadges(Score, Score.Hole18, Course.Par18);
        }
        private int getCallawayHcp(Score Score, Course Course)
        {
            int points = 36;
            points = points - getCallawayPoints(Score.Hole01, Course.Par01);
            points = points - getCallawayPoints(Score.Hole02, Course.Par02);
            points = points - getCallawayPoints(Score.Hole03, Course.Par03);
            points = points - getCallawayPoints(Score.Hole04, Course.Par04);
            points = points - getCallawayPoints(Score.Hole05, Course.Par05);
            points = points - getCallawayPoints(Score.Hole06, Course.Par06);
            points = points - getCallawayPoints(Score.Hole07, Course.Par07);
            points = points - getCallawayPoints(Score.Hole08, Course.Par08);
            points = points - getCallawayPoints(Score.Hole09, Course.Par09);
            points = points - getCallawayPoints(Score.Hole10, Course.Par10);
            points = points - getCallawayPoints(Score.Hole11, Course.Par11);
            points = points - getCallawayPoints(Score.Hole12, Course.Par12);
            points = points - getCallawayPoints(Score.Hole13, Course.Par13);
            points = points - getCallawayPoints(Score.Hole14, Course.Par14);
            points = points - getCallawayPoints(Score.Hole15, Course.Par15);
            points = points - getCallawayPoints(Score.Hole16, Course.Par16);
            points = points - getCallawayPoints(Score.Hole17, Course.Par17);
            points = points - getCallawayPoints(Score.Hole18, Course.Par18);
            return points;
        }
        private int getCallawayPoints(int Score, int Par)
        {
            if ((Score - Par) >= 2) return 0;
            if ((Score - Par) == 1) return 1;
            return 2;
        }
        private void countBadges(Score Score, int HoleScore, int ParScore)
        {
            if (HoleScore == 1) Score.HoleInOnes++;
            if (HoleScore >= (ParScore * 2 + 1)) Score.MaxScore++;

            int score = HoleScore - ParScore;
            switch (score)
            {
                case -3:
                    Score.Albatross++;
                    break;
                case -2:
                    Score.Eagles++;
                    break;
                case -1:
                    Score.Birdies++;
                    break;
                case 0:
                    Score.Pars++;
                    break;
                case 1:
                    Score.Bogeys++;
                    break;
                case 2:
                    Score.DoubleBogeys++;
                    break;
                case 3:
                    Score.TripleBogeys++;
                    break;
                case 4:
                    Score.QuadBogeys++;
                    break;
                default:
                    Score.Disaster++;
                    break;
            }
        }

    }
}
