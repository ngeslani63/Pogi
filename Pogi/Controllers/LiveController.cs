using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Services;
using Pogi.Models.LiveViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class LiveController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IScoreInfo _scoreInfo;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberData _memberData;
        private readonly ICourseData _courseData;
        private readonly ICourseDetail _courseDetail;
        private readonly ICourseHandicap _courseHandicap;
        private readonly IHandicap _handicap;
        private readonly ICourseMap _courseMap;
        private readonly ITourInfo _tourInfo;
        private readonly ITourDay _tourDayInfo;
        private readonly ITeeTimeInfo _teeTimeInfo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActivity _activity;
        private readonly IDateTime _dateTime;
        private ISession _session => _httpContextAccessor.HttpContext.Session; 
      
        public LiveController(PogiDbContext context, IScoreInfo scoreInfo,
             SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager, IMemberData memberData, ICourseData courseData,
                ICourseDetail courseDetail,
                ICourseHandicap courseHandicap,
                IHandicap handicap,
                ICourseMap courseMap,
                ITourInfo tourInfo,
                ITourDay tourDayInfo,
                ITeeTimeInfo teeTimeInfo,
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
            _courseHandicap = courseHandicap;
            _handicap = handicap;
            _courseMap = courseMap;
            _tourInfo = tourInfo;
            _tourDayInfo = tourDayInfo;
            _teeTimeInfo = teeTimeInfo;
            _httpContextAccessor = httpContextAccessor;
            _activity = activity;
            _dateTime = dateTime;

     
        }
        public IActionResult MapHole(string TourId, string TourDate, string memberId, string tGroup, string pGroup,
    string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4,
    int posP1 = 1, int posP2 = 2, int posP3 = 3, int posP4 = 4, int tHole = 1)
        {
            ViewBag.TourId = TourId;
            ViewBag.TourDate = TourDate;
            ViewBag.memberId = memberId;
            ViewBag.tGroup = tGroup;
            ViewBag.pGroup = pGroup;
            ViewBag.sMemberId1 = sMemberId1;
            ViewBag.sMemberId2 = sMemberId2;
            ViewBag.sMemberId3 = sMemberId3;
            ViewBag.sMemberId4 = sMemberId4;
            ViewBag.posP1 = posP1;
            ViewBag.posP2 = posP2;
            ViewBag.posP3 = posP3;
            ViewBag.posP4 = posP4;
            ViewBag.tHole = tHole;


            //Boolean redirect = false;
            Tour tour = null;
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLive");
                if (TourId == null || TourId.Length == 0)
                {
                    tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                        //redirect = true;
                    }
                    else
                    {
                        TourId = "1";
                    }
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    _session.SetString("TourIdLive", TourId);
                }
                else
                {
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    //redirect = true;
                }
            }
            else
            {
                tour = _tourInfo.getTour(int.Parse(TourId));
                string prevTourId = _session.GetString("TourIdLive");
                if (prevTourId != null && TourId != prevTourId)
                {
                    TourDate = "";
                    _session.Remove("TourDateLive");
                }
                _session.SetString("TourIdLive", TourId);
            }
            if (TourDate == null || TourDate.Length == 0)
            {
                TourDate = _session.GetString("TourDateLive");
                if (TourDate == null || TourDate.Length == 0)
                {
                    if (tour != null)
                    {
                        if (tour.TourType == TourType.SingleDay)
                        {
                            TourDate = tour.TourDate.ToShortDateString();
                            //redirect = true;
                        }
                        else
                        {
                            TourDay tourDay = _tourDayInfo.GetLatestTourDay(tour.TourId);
                            if (tourDay != null)
                            {
                                TourDate = tourDay.TourDate.ToShortDateString();
                                //redirect = true;
                            }
                            else
                            {
                                TourDate = "";
                            }
                        }
                    }
                    _session.SetString("TourDateLive", TourDate);
                }
                else
                {
                    //redirect = true;
                }
            }
            else
            {
                _session.SetString("TourDateLive", TourDate);
            }
            if (tour == null)
            {
                return RedirectToAction("Index", "Live");
            }
            
            
            var model = new LiveMapHoleViewModel();
            model.Tour = tour;

            TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate,
                CultureInfo.CurrentCulture));
            model.Course = _courseData.get(teeTime.CourseId);
            model.Hole = (short)tHole;
            CourseMap courseMap = _courseMap.get(teeTime.CourseId);
            if (courseMap == null)
            {
                courseMap = new CourseMap();
            }
            if (courseMap != null)
            {
                model.Lat01 = courseMap.Lat01;
                model.Lat02 = courseMap.Lat02;
                model.Lat03 = courseMap.Lat03;
                model.Lat04 = courseMap.Lat04;
                model.Lat05 = courseMap.Lat05;
                model.Lat06 = courseMap.Lat06;
                model.Lat07 = courseMap.Lat07;
                model.Lat08 = courseMap.Lat08;
                model.Lat09 = courseMap.Lat09;
                model.Lat10 = courseMap.Lat10;
                model.Lat11 = courseMap.Lat11;
                model.Lat12 = courseMap.Lat12;
                model.Lat13 = courseMap.Lat13;
                model.Lat14 = courseMap.Lat14;
                model.Lat15 = courseMap.Lat15;
                model.Lat16 = courseMap.Lat16;
                model.Lat17 = courseMap.Lat17;
                model.Lat18 = courseMap.Lat18;

                model.Lng01 = courseMap.Lng01;
                model.Lng02 = courseMap.Lng02;
                model.Lng03 = courseMap.Lng03;
                model.Lng04 = courseMap.Lng04;
                model.Lng05 = courseMap.Lng05;
                model.Lng06 = courseMap.Lng06;
                model.Lng07 = courseMap.Lng07;
                model.Lng08 = courseMap.Lng08;
                model.Lng09 = courseMap.Lng09;
                model.Lng10 = courseMap.Lng10;
                model.Lng11 = courseMap.Lng11;
                model.Lng12 = courseMap.Lng12;
                model.Lng13 = courseMap.Lng13;
                model.Lng14 = courseMap.Lng14;
                model.Lng15 = courseMap.Lng15;
                model.Lng16 = courseMap.Lng16;
                model.Lng17 = courseMap.Lng17;
                model.Lng18 = courseMap.Lng18;

                model.InitLat01 = courseMap.InitLat01;
                model.InitLat02 = courseMap.InitLat02;
                model.InitLat03 = courseMap.InitLat03;
                model.InitLat04 = courseMap.InitLat04;
                model.InitLat05 = courseMap.InitLat05;
                model.InitLat06 = courseMap.InitLat06;
                model.InitLat07 = courseMap.InitLat07;
                model.InitLat08 = courseMap.InitLat08;
                model.InitLat09 = courseMap.InitLat09;
                model.InitLat10 = courseMap.InitLat10;
                model.InitLat11 = courseMap.InitLat11;
                model.InitLat12 = courseMap.InitLat12;
                model.InitLat13 = courseMap.InitLat13;
                model.InitLat14 = courseMap.InitLat14;
                model.InitLat15 = courseMap.InitLat15;
                model.InitLat16 = courseMap.InitLat16;
                model.InitLat17 = courseMap.InitLat17;
                model.InitLat18 = courseMap.InitLat18;

                model.InitLng01 = courseMap.InitLng01;
                model.InitLng02 = courseMap.InitLng02;
                model.InitLng03 = courseMap.InitLng03;
                model.InitLng04 = courseMap.InitLng04;
                model.InitLng05 = courseMap.InitLng05;
                model.InitLng06 = courseMap.InitLng06;
                model.InitLng07 = courseMap.InitLng07;
                model.InitLng08 = courseMap.InitLng08;
                model.InitLng09 = courseMap.InitLng09;
                model.InitLng10 = courseMap.InitLng10;
                model.InitLng11 = courseMap.InitLng11;
                model.InitLng12 = courseMap.InitLng12;
                model.InitLng13 = courseMap.InitLng13;
                model.InitLng14 = courseMap.InitLng14;
                model.InitLng15 = courseMap.InitLng15;
                model.InitLng16 = courseMap.InitLng16;
                model.InitLng17 = courseMap.InitLng17;
                model.InitLng18 = courseMap.InitLng18;
                model.Hdg01 = courseMap.Hdg01;
                model.Hdg02 = courseMap.Hdg02;
                model.Hdg03 = courseMap.Hdg03;
                model.Hdg04 = courseMap.Hdg04;
                model.Hdg05 = courseMap.Hdg05;
                model.Hdg06 = courseMap.Hdg06;
                model.Hdg07 = courseMap.Hdg07;
                model.Hdg08 = courseMap.Hdg08;
                model.Hdg09 = courseMap.Hdg09;
                model.Hdg10 = courseMap.Hdg10;
                model.Hdg11 = courseMap.Hdg11;
                model.Hdg12 = courseMap.Hdg12;
                model.Hdg13 = courseMap.Hdg13;
                model.Hdg14 = courseMap.Hdg14;
                model.Hdg15 = courseMap.Hdg15;
                model.Hdg16 = courseMap.Hdg16;
                model.Hdg17 = courseMap.Hdg17;
                model.Hdg18 = courseMap.Hdg18;
                model.Zoom01 = courseMap.Zoom01;
                model.Zoom02 = courseMap.Zoom02;
                model.Zoom03 = courseMap.Zoom03;
                model.Zoom04 = courseMap.Zoom04;
                model.Zoom05 = courseMap.Zoom05;
                model.Zoom06 = courseMap.Zoom06;
                model.Zoom07 = courseMap.Zoom07;
                model.Zoom08 = courseMap.Zoom08;
                model.Zoom09 = courseMap.Zoom09;
                model.Zoom10 = courseMap.Zoom10;
                model.Zoom11 = courseMap.Zoom11;
                model.Zoom12 = courseMap.Zoom12;
                model.Zoom13 = courseMap.Zoom13;
                model.Zoom14 = courseMap.Zoom14;
                model.Zoom15 = courseMap.Zoom15;
                model.Zoom16 = courseMap.Zoom16;
                model.Zoom17 = courseMap.Zoom17;
                model.Zoom18 = courseMap.Zoom18;
                switch (tHole)
                {
                    case 01:
                        model.Lat = courseMap.Lat01;
                        model.Lng = courseMap.Lng01;
                        model.InitLat = courseMap.InitLat01;
                        model.InitLng = courseMap.InitLng01;
                        model.Hdg = courseMap.Hdg01;
                        model.Zoom = courseMap.Zoom01;
                        break;
                    case 02:
                        model.Lat = courseMap.Lat02;
                        model.Lng = courseMap.Lng02;
                        model.InitLat = courseMap.InitLat02;
                        model.InitLng = courseMap.InitLng02;
                        model.Hdg = courseMap.Hdg02;
                        model.Zoom = courseMap.Zoom02;
                        break;
                    case 03:
                        model.Lat = courseMap.Lat03;
                        model.Lng = courseMap.Lng03;
                        model.InitLat = courseMap.InitLat03;
                        model.InitLng = courseMap.InitLng03;
                        model.Hdg = courseMap.Hdg03;
                        model.Zoom = courseMap.Zoom03;
                        break;
                    case 04:
                        model.Lat = courseMap.Lat04;
                        model.Lng = courseMap.Lng04;
                        model.InitLat = courseMap.InitLat04;
                        model.InitLng = courseMap.InitLng04;
                        model.Hdg = courseMap.Hdg04;
                        model.Zoom = courseMap.Zoom04;
                        break;
                    case 05:
                        model.Lat = courseMap.Lat05;
                        model.Lng = courseMap.Lng05;
                        model.InitLat = courseMap.InitLat05;
                        model.InitLng = courseMap.InitLng05;
                        model.Hdg = courseMap.Hdg05;
                        model.Zoom = courseMap.Zoom05;
                        break;
                    case 06:
                        model.Lat = courseMap.Lat06;
                        model.Lng = courseMap.Lng06;
                        model.InitLat = courseMap.InitLat06;
                        model.InitLng = courseMap.InitLng06;
                        model.Hdg = courseMap.Hdg06;
                        model.Zoom = courseMap.Zoom06;
                        break;
                    case 07:
                        model.Lat = courseMap.Lat07;
                        model.Lng = courseMap.Lng07;
                        model.InitLat = courseMap.InitLat07;
                        model.InitLng = courseMap.InitLng07;
                        model.Hdg = courseMap.Hdg07;
                        model.Zoom = courseMap.Zoom07;
                        break;
                    case 08:
                        model.Lat = courseMap.Lat08;
                        model.Lng = courseMap.Lng08;
                        model.InitLat = courseMap.InitLat08;
                        model.InitLng = courseMap.InitLng08;
                        model.Hdg = courseMap.Hdg08;
                        model.Zoom = courseMap.Zoom08;
                        break;
                    case 09:
                        model.Lat = courseMap.Lat09;
                        model.Lng = courseMap.Lng09;
                        model.InitLat = courseMap.InitLat09;
                        model.InitLng = courseMap.InitLng09;
                        model.Hdg = courseMap.Hdg09;
                        model.Zoom = courseMap.Zoom09;
                        break;
                    case 10:
                        model.Lat = courseMap.Lat10;
                        model.Lng = courseMap.Lng10;
                        model.InitLat = courseMap.InitLat10;
                        model.InitLng = courseMap.InitLng10;
                        model.Hdg = courseMap.Hdg10;
                        model.Zoom = courseMap.Zoom10;
                        break;
                    case 11:
                        model.Lat = courseMap.Lat11;
                        model.Lng = courseMap.Lng11;
                        model.InitLat = courseMap.InitLat11;
                        model.InitLng = courseMap.InitLng11;
                        model.Hdg = courseMap.Hdg11;
                        model.Zoom = courseMap.Zoom11;
                        break;
                    case 12:
                        model.Lat = courseMap.Lat12;
                        model.Lng = courseMap.Lng12;
                        model.InitLat = courseMap.InitLat12;
                        model.InitLng = courseMap.InitLng12;
                        model.Hdg = courseMap.Hdg12;
                        model.Zoom = courseMap.Zoom12;
                        break;
                    case 13:
                        model.Lat = courseMap.Lat13;
                        model.Lng = courseMap.Lng13;
                        model.InitLat = courseMap.InitLat13;
                        model.InitLng = courseMap.InitLng13;
                        model.Hdg = courseMap.Hdg13;
                        model.Zoom = courseMap.Zoom13;
                        break;
                    case 14:
                        model.Lat = courseMap.Lat14;
                        model.Lng = courseMap.Lng14;
                        model.InitLat = courseMap.InitLat14;
                        model.InitLng = courseMap.InitLng14;
                        model.Hdg = courseMap.Hdg14;
                        model.Zoom = courseMap.Zoom14;
                        break;
                    case 15:
                        model.Lat = courseMap.Lat15;
                        model.Lng = courseMap.Lng15;
                        model.InitLat = courseMap.InitLat15;
                        model.InitLng = courseMap.InitLng15;
                        model.Hdg = courseMap.Hdg15;
                        model.Zoom = courseMap.Zoom15;
                        break;
                    case 16:
                        model.Lat = courseMap.Lat16;
                        model.Lng = courseMap.Lng16;
                        model.InitLat = courseMap.InitLat16;
                        model.InitLng = courseMap.InitLng16;
                        model.Hdg = courseMap.Hdg16;
                        model.Zoom = courseMap.Zoom16;
                        break;
                    case 17:
                        model.Lat = courseMap.Lat17;
                        model.Lng = courseMap.Lng17;
                        model.InitLat = courseMap.InitLat17;
                        model.InitLng = courseMap.InitLng17;
                        model.Hdg = courseMap.Hdg17;
                        model.Zoom = courseMap.Zoom17;
                        break;
                    case 18:
                        model.Lat = courseMap.Lat18;
                        model.Lng = courseMap.Lng18;
                        model.InitLat = courseMap.InitLat18;
                        model.InitLng = courseMap.InitLng18;
                        model.Hdg = courseMap.Hdg18;
                        model.Zoom = courseMap.Zoom18;
                        break;
                    default:
                        model.Lat = courseMap.Lat01;
                        model.Lng = courseMap.Lng01;
                        model.InitLat = courseMap.InitLat01;
                        model.InitLng = courseMap.InitLng01;
                        model.Hdg = courseMap.Hdg01;
                        model.Zoom = courseMap.Zoom01;
                        break;
                }
            }
            else
            {
                courseMap = new CourseMap();
                model.Zoom = courseMap.Zoom;
                model.Lat = courseMap.Lat01;    
                model.Lng = courseMap.Lng01;
                model.InitLat = courseMap.InitLat01;
                model.InitLng = courseMap.InitLng01;
                model.Hdg = courseMap.Hdg01;
            }

            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live MapHole");
            return View(model);
        }
        public IActionResult Leaderboard(string TourId, string TourDate, string memberId, string tGroup, string pGroup,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4,
            int posP1 = 1, int posP2 = 2 , int posP3 = 3, int posP4 = 4)
        {
            ViewBag.TourId = TourId;
            ViewBag.TourDate = TourDate;
            ViewBag.memberId = memberId;
            ViewBag.tGroup = tGroup;
            ViewBag.pGroup = pGroup;
            ViewBag.sMemberId1 = sMemberId1;
            ViewBag.sMemberId2 = sMemberId2;
            ViewBag.sMemberId3 = sMemberId3;
            ViewBag.sMemberId4 = sMemberId4;
            ViewBag.posP1 = posP1;
            ViewBag.posP2 = posP2;
            ViewBag.posP3 = posP3;
            ViewBag.posP4 = posP4;
            

            //Boolean redirect = false;
            Tour tour = null;
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLive");
                if (TourId == null || TourId.Length == 0)
                {
                    tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                        //redirect = true;
                    }
                    else
                    {
                        TourId = "1";
                    }
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    _session.SetString("TourIdLive", TourId);
                }
                else
                {
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    //redirect = true;
                }
            }
            else
            {
                tour = _tourInfo.getTour(int.Parse(TourId));
                string prevTourId = _session.GetString("TourIdLive");
                if (prevTourId != null && TourId != prevTourId)
                {
                    TourDate = "";
                    _session.Remove("TourDateLive");
                }
                _session.SetString("TourIdLive", TourId);
            }
            if (TourDate == null || TourDate.Length == 0)
            {
                TourDate = _session.GetString("TourDateLive");
                if (TourDate == null || TourDate.Length == 0)
                {
                    if (tour != null)
                    {
                        if (tour.TourType == TourType.SingleDay)
                        {
                            TourDate = tour.TourDate.ToShortDateString();
                            //redirect = true;
                        }
                        else
                        {
                            TourDay tourDay = _tourDayInfo.GetLatestTourDay(tour.TourId);
                            if (tourDay != null)
                            {
                                TourDate = tourDay.TourDate.ToShortDateString();
                                //redirect = true;
                            }
                            else
                            {
                                TourDate = "";
                            }
                        }
                    }
                    _session.SetString("TourDateLive", TourDate);
                }
                else
                {
                    //redirect = true;
                }
            }
            else
            {
                _session.SetString("TourDateLive", TourDate);
            }
            if (tour == null)  
            {
                return RedirectToAction("Select", "Live", new { TourDate = TourDate, TourId = TourId });
            }
            var model = new LiveLeaderBoardViewModel();
            model.Tour = tour;
            if (TourId.Length > 0 && int.Parse(TourId) > 0)
            {
                if (tour != null && tour.TourType == TourType.MultiDay)
                {
                    model.ScoreInfos = _scoreInfo.getForTour(int.Parse(TourId), DateTime.Parse(TourDate).Date);
                }
                else
                {
                    model.ScoreInfos = _scoreInfo.getForTour(int.Parse(TourId));
                }

                model.TourId = TourId;
                model.TourDate = TourDate;
            }

            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live Leaderboard");
            return View(model);
        }

        public IActionResult Index(string TourId, string TourDate, string memberId, string tGroup, string pGroup,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4,
            int posP1 = 1, int posP2 = 2, int posP3 = 3, int posP4 = 4)
        {
            ViewBag.TourId = TourId;
            ViewBag.TourDate = TourDate;
            ViewBag.memberId = memberId;
            ViewBag.tGroup = tGroup;
            ViewBag.pGroup = pGroup;
            ViewBag.sMemberId1 = sMemberId1;
            ViewBag.sMemberId2 = sMemberId2;
            ViewBag.sMemberId3 = sMemberId3;
            ViewBag.sMemberId4 = sMemberId4;
            ViewBag.posP1 = posP1;
            ViewBag.posP2 = posP2;
            ViewBag.posP3 = posP3;
            ViewBag.posP4 = posP4;
            Boolean redirect = false;
            Tour tour = null;
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLive");
                if (TourId == null || TourId.Length == 0)
                {
                    tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                        redirect = true;
                    }
                    else
                    {
                        TourId = "1";
                    }
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    _session.SetString("TourIdLive", TourId);
                }
                else
                {
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    redirect = true;
                }
            }
            else
            {
                tour = _tourInfo.getTour(int.Parse(TourId));
                string prevTourId = _session.GetString("TourIdLive");
                if (prevTourId != null && TourId != prevTourId)
                {
                    TourDate = "";
                    _session.Remove("TourDateLive");
                }
                _session.SetString("TourIdLive", TourId);
            }
            if (TourDate == null || TourDate.Length == 0)
            {
                TourDate = _session.GetString("TourDateLive");
                if (TourDate == null || TourDate.Length == 0)
                {
                    if (tour != null)
                    {
                        if (tour.TourType == TourType.SingleDay)
                        {
                            TourDate = tour.TourDate.ToShortDateString();
                            redirect = true;
                        }
                        else
                        {
                            TourDay tourDay = _tourDayInfo.GetLatestTourDay(tour.TourId);
                            if (tourDay != null)
                            {
                                TourDate = tourDay.TourDate.ToShortDateString();
                                redirect = true;
                            }
                            else
                            {
                                TourDate = "";
                            }
                        }
                    }
                    _session.SetString("TourDateLive", TourDate);
                }
                else
                {
                    redirect = true;
                }
            }
            else
            {
                _session.SetString("TourDateLive", TourDate);
            }
            if (redirect)
            {
                return RedirectToAction("Index", "Live", new { TourDate = TourDate, TourId = TourId,
                    posP1 = posP1, posP2 = posP2, posP3 = posP3, posP4 = posP4});
            }
            var model = new LiveSelectViewModel();
            if (TourDate != null)
            {
                TeeTime TeeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate).Date);
                if (TeeTime != null)
                {
                    model.TeeTimeInfo = _teeTimeInfo.getTeeTimeInfo(TeeTime);
                }
                model.Tour = _tourInfo.getTour(int.Parse(TourId));
                model.TourId = TourId;
                model.TourDate = TourDate;
            }
            model.Tours = _tourInfo.getTours(false, false);
            model.TourDates = _tourDayInfo.getSelectList(int.Parse(model.TourId));
            if (TourDate != null && TourDate.Length > 0)
            {
                model.TourDates = _tourDayInfo.getSelectList(int.Parse(model.TourId), DateTime.Parse(TourDate).Date, true);
            }
            else
            {
                model.TourDates = _tourDayInfo.getSelectList(int.Parse(model.TourId));
            }

            TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate,
                CultureInfo.CurrentCulture));
            model.Course = _courseData.get(teeTime.CourseId);
            CourseHandicap courseHandicap = _courseHandicap.get(teeTime.CourseId);
            model.BaseCourseDetail = _courseDetail.get(model.Course.CourseId, model.Tour.BaseColor.ToString());

            if (model.Tour.ScorerType.ToString().Contains("Ryder"))
            {
                int[] cHcps = new int[18];
                cHcps[0] = courseHandicap.MenHcp01;
                cHcps[1] = courseHandicap.MenHcp02;
                cHcps[2] = courseHandicap.MenHcp03;
                cHcps[3] = courseHandicap.MenHcp04;
                cHcps[4] = courseHandicap.MenHcp05;
                cHcps[5] = courseHandicap.MenHcp06;
                cHcps[6] = courseHandicap.MenHcp07;
                cHcps[7] = courseHandicap.MenHcp08;
                cHcps[8] = courseHandicap.MenHcp09;
                cHcps[9] = courseHandicap.MenHcp10;
                cHcps[10] = courseHandicap.MenHcp11;
                cHcps[11] = courseHandicap.MenHcp12;
                cHcps[12] = courseHandicap.MenHcp13;
                cHcps[13] = courseHandicap.MenHcp14;
                cHcps[14] = courseHandicap.MenHcp15;
                cHcps[15] = courseHandicap.MenHcp16;
                cHcps[16] = courseHandicap.MenHcp17;
                cHcps[17] = courseHandicap.MenHcp18;
                model.rankedHoles = new int[18];
                for (int i = 0; i < 18; i++)
                {
                    pushCHcps(model.rankedHoles, cHcps, i);
                }
            }

            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live Select Group");
            return View(model);
        }
        private void pushCHcps(int[] rankedHoles, int[] cHcps, int i)
        {
            rankedHoles[cHcps[i] - 1] = i+1;     // Hole 06 = Hcp 01  cHcp(05) = 1
        }
        public IActionResult Score(string TourId, string TourDate, string memberId, string tGroup, string pGroup, string tPlayer,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4,
              int posP1 = 1, int posP2 = 2, int posP3 = 3, int posP4 = 4)
        {
            ViewBag.TourId = TourId;
            ViewBag.TourDate = TourDate;
            ViewBag.memberId = memberId;
            ViewBag.tGroup = tGroup;
            ViewBag.pGroup = pGroup;
            ViewBag.sMemberId1 = sMemberId1;
            ViewBag.sMemberId2 = sMemberId2;
            ViewBag.sMemberId3 = sMemberId3;
            ViewBag.sMemberId4 = sMemberId4;
            ViewBag.posP1 = posP1;
            ViewBag.posP2 = posP2;
            ViewBag.posP3 = posP3;
            ViewBag.posP4 = posP4;

            //Boolean redirect = false;
            Tour tour = null;
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdLive");
                if (TourId == null || TourId.Length == 0)
                {
                    tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                        //redirect = true;
                    }
                    else
                    {
                        TourId = "1";
                    }
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    _session.SetString("TourIdLive", TourId);
                }
                else
                {
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    //redirect = true;
                }
            }
            else
            {
                tour = _tourInfo.getTour(int.Parse(TourId));
                string prevTourId = _session.GetString("TourIdLive");
                if (prevTourId != null && TourId != prevTourId)
                {
                    TourDate = "";
                    _session.Remove("TourDateLive");
                }
                _session.SetString("TourIdLive", TourId);
            }
            if (TourDate == null || TourDate.Length == 0)
            {
                TourDate = _session.GetString("TourDateLive");
                if (TourDate == null || TourDate.Length == 0)
                {
                    if (tour != null)
                    {
                        if (tour.TourType == TourType.SingleDay)
                        {
                            TourDate = tour.TourDate.ToShortDateString();
                            //redirect = true;
                        }
                        else
                        {
                            TourDay tourDay = _tourDayInfo.GetLatestTourDay(tour.TourId);
                            if (tourDay != null)
                            {
                                TourDate = tourDay.TourDate.ToShortDateString();
                                //redirect = true;
                            }
                            else
                            {
                                TourDate = "";
                            }
                        }
                    }
                    _session.SetString("TourDateLive", TourDate);
                }
                else
                {
                    //redirect = true;
                }
            }
            else
            {
                _session.SetString("TourDateLive", TourDate);
            }
            if (tour == null)
            {
                return RedirectToAction("Index", "Live");
            }
            int currPlayer = 1;
            string[] sPlayer = { sMemberId1, sMemberId2, sMemberId3, sMemberId4 };
            int[] player = { 0, 0, 0, 0 };
            int pCnt = 0;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (sPlayer[i] != null) {
                        int mId = int.Parse(sPlayer[i]);
                        if (mId > 0)
                        {
                            pCnt++;
                            player[pCnt - 1] = mId;
                           
                            if (tPlayer != null)
                            {
                                if ((int.Parse(tPlayer) -1) == i)
                                {
                                    currPlayer = pCnt;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (pCnt == 0)
            {
                return RedirectToAction("Index", "Live");
            }
            var model = new LiveScoreViewModel();
            model.Tour = tour;
            TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate,
                CultureInfo.CurrentCulture));
            model.Course = _courseData.get(teeTime.CourseId);
            Member user = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            model.currPlayer = currPlayer.ToString();

            for (int i = 0; i < pCnt; i++)
            {
                Member member = _memberData.get(player[i]);

                model.Players.Add(member);
                Score score = PrimeScore(DateTime.Parse(TourDate, CultureInfo.CurrentCulture),
                               member, model.Course, tour, user);
                model.Scores.Add(score) ;
            }

            model.nextHole = "1";
            Score sc = model.Scores[currPlayer-1];
            if (sc != null)
            {
                if (sc.Hole18 == 0) model.nextHole = "18";
                if (sc.Hole17 == 0) model.nextHole = "17";
                if (sc.Hole16 == 0) model.nextHole = "16";
                if (sc.Hole15 == 0) model.nextHole = "15";
                if (sc.Hole14 == 0) model.nextHole = "14";
                if (sc.Hole13 == 0) model.nextHole = "13";
                if (sc.Hole12 == 0) model.nextHole = "12";
                if (sc.Hole11 == 0) model.nextHole = "11";
                if (sc.Hole10 == 0) model.nextHole = "10";
                if (sc.Hole09 == 0) model.nextHole = "9";
                if (sc.Hole08 == 0) model.nextHole = "8";
                if (sc.Hole07 == 0) model.nextHole = "7";
                if (sc.Hole06 == 0) model.nextHole = "6";
                if (sc.Hole05 == 0) model.nextHole = "5";
                if (sc.Hole04 == 0) model.nextHole = "4";
                if (sc.Hole03 == 0) model.nextHole = "3";
                if (sc.Hole02 == 0) model.nextHole = "2";
                if (sc.Hole01 == 0) model.nextHole = "1";
            }

            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live Score");
            return View(model);
        }

        public IActionResult RyderCup(string TourId, string TourDate, string memberId, string tGroup, string pGroup, string tPlayer,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4, string cHcpDiff,
            string rH1, string rH2, string rH3, string rH4, string rH5, string rH6, string rH7, string rH8, string rH9,
            string rH10, string rH11, string rH12, string rH13, string rH14, string rH15, string rH16, string rH17, string rH18,
            int posP1 = 1, int posP2 = 2, int posP3 = 3, int posP4 = 4)
        {
            ViewBag.TourId = TourId;
            ViewBag.TourDate = TourDate;
            ViewBag.memberId = memberId;
            ViewBag.tGroup = tGroup;
            ViewBag.pGroup = pGroup;
            ViewBag.sMemberId1 = sMemberId1;
            ViewBag.sMemberId2 = sMemberId2;
            ViewBag.sMemberId3 = sMemberId3;
            ViewBag.sMemberId4 = sMemberId4;
            ViewBag.cHcpDiff = cHcpDiff;
            ViewBag.rH1 = rH1;
            ViewBag.rH2 = rH2;
            ViewBag.rH3 = rH3;
            ViewBag.rH4 = rH4;
            ViewBag.rH5 = rH5;
            ViewBag.rH6 = rH6;
            ViewBag.rH7 = rH7;
            ViewBag.rH8 = rH8;
            ViewBag.rH9 = rH9;
            ViewBag.rH10 = rH10;
            ViewBag.rH11 = rH11;
            ViewBag.rH12 = rH12;
            ViewBag.rH13 = rH13;
            ViewBag.rH14 = rH14;
            ViewBag.rH15 = rH15;
            ViewBag.rH16 = rH16;
            ViewBag.rH17 = rH17;
            ViewBag.rH18 = rH18;
            ViewBag.posP1 = posP1;
            ViewBag.posP2 = posP2;
            ViewBag.posP3 = posP3;
            ViewBag.posP4 = posP4;

            //Boolean redirect = false;
            Tour tour = null;
            if (TourId == null || TourId.Length == 0)
            {
                TourId = _session.GetString("TourIdRyderCup");
                if (TourId == null || TourId.Length == 0)
                {
                    tour = _tourInfo.getLatestTour();
                    if (tour != null)
                    {
                        TourId = tour.TourId.ToString();
                        //redirect = true;
                    }
                    else
                    {
                        TourId = "1";
                    }
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    _session.SetString("TourIdRyderCup", TourId);
                }
                else
                {
                    tour = _tourInfo.getTour(int.Parse(TourId));
                    //redirect = true;
                }
            }
            else
            {
                tour = _tourInfo.getTour(int.Parse(TourId));
                string prevTourId = _session.GetString("TourIdRyderCup");
                if (prevTourId != null && TourId != prevTourId)
                {
                    TourDate = "";
                    _session.Remove("TourDateRyderCup");
                }
                _session.SetString("TourIdRyderCup", TourId);
            }
            if (TourDate == null || TourDate.Length == 0)
            {
                TourDate = _session.GetString("TourDateRyderCup");
                if (TourDate == null || TourDate.Length == 0)
                {
                    if (tour != null)
                    {
                        if (tour.TourType == TourType.SingleDay)
                        {
                            TourDate = tour.TourDate.ToShortDateString();
                            //redirect = true;
                        }
                        else
                        {
                            TourDay tourDay = _tourDayInfo.GetLatestTourDay(tour.TourId);
                            if (tourDay != null)
                            {
                                TourDate = tourDay.TourDate.ToShortDateString();
                                //redirect = true;
                            }
                            else
                            {
                                TourDate = "";
                            }
                        }
                    }
                    _session.SetString("TourDateRyderCup", TourDate);
                }
                else
                {
                    //redirect = true;
                }
            }
            else
            {
                _session.SetString("TourDateRyderCup", TourDate);
            }
            if (tour == null)
            {
                return RedirectToAction("RyderCup", "Live");
            }
            int currPlayer = 1;
            string[] sPlayer = { sMemberId1, sMemberId2, sMemberId3, sMemberId4 };
            int[] player = { 0, 0, 0, 0 };
            int pCnt = 0;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (sPlayer[i] != null)
                    {
                        int mId = int.Parse(sPlayer[i]);
                        if (mId > 0)
                        {
                            pCnt++;
                            player[pCnt - 1] = mId;

                            if (tPlayer != null)
                            {
                                if ((int.Parse(tPlayer) - 1) == i)
                                {
                                    currPlayer = pCnt;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (currPlayer == 1) currPlayer = 2;
            else currPlayer = 4;
            if (pCnt == 0)
            {
                return RedirectToAction("RyderCup", "Live");
            }
            var model = new LiveScoreViewModel();
            model.Tour = tour;
            TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate,
                CultureInfo.CurrentCulture));
            model.Course = _courseData.get(teeTime.CourseId);
            Member user = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            model.currPlayer = currPlayer.ToString();

            for (int i = 0; i < pCnt; i++)
            {
                Member member = _memberData.get(player[i]);

                model.Players.Add(member);
                Score score = PrimeScore(DateTime.Parse(TourDate, CultureInfo.CurrentCulture),
                               member, model.Course, tour, user);
                model.Scores.Add(score);
            }

            model.nextHole = "1";
            Score sc = model.Scores[currPlayer - 1];
            if (sc != null)
            {
                if (sc.Hole18 == 0) model.nextHole = "18";
                if (sc.Hole17 == 0) model.nextHole = "17";
                if (sc.Hole16 == 0) model.nextHole = "16";
                if (sc.Hole15 == 0) model.nextHole = "15";
                if (sc.Hole14 == 0) model.nextHole = "14";
                if (sc.Hole13 == 0) model.nextHole = "13";
                if (sc.Hole12 == 0) model.nextHole = "12";
                if (sc.Hole11 == 0) model.nextHole = "11";
                if (sc.Hole10 == 0) model.nextHole = "10";
                if (sc.Hole09 == 0) model.nextHole = "9";
                if (sc.Hole08 == 0) model.nextHole = "8";
                if (sc.Hole07 == 0) model.nextHole = "7";
                if (sc.Hole06 == 0) model.nextHole = "6";
                if (sc.Hole05 == 0) model.nextHole = "5";
                if (sc.Hole04 == 0) model.nextHole = "4";
                if (sc.Hole03 == 0) model.nextHole = "3";
                if (sc.Hole02 == 0) model.nextHole = "2";
                if (sc.Hole01 == 0) model.nextHole = "1";
            }


            model.rankedHoles = new int[18];
            model.rankedHoles[0] = Int32.Parse(rH1);
            model.rankedHoles[1] = Int32.Parse(rH2);
            model.rankedHoles[2] = Int32.Parse(rH3);
            model.rankedHoles[3] = Int32.Parse(rH4);
            model.rankedHoles[4] = Int32.Parse(rH5);
            model.rankedHoles[5] = Int32.Parse(rH6);
            model.rankedHoles[6] = Int32.Parse(rH7);
            model.rankedHoles[7] = Int32.Parse(rH8);
            model.rankedHoles[8] = Int32.Parse(rH9);
            model.rankedHoles[9] = Int32.Parse(rH10);
            model.rankedHoles[10] = Int32.Parse(rH11);
            model.rankedHoles[11] = Int32.Parse(rH12);
            model.rankedHoles[12] = Int32.Parse(rH13);
            model.rankedHoles[13] = Int32.Parse(rH14);
            model.rankedHoles[14] = Int32.Parse(rH15);
            model.rankedHoles[15] = Int32.Parse(rH16);
            model.rankedHoles[16] = Int32.Parse(rH17);
            model.rankedHoles[17] = Int32.Parse(rH18);

            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live RyderCup");
            return View(model);
        }
        private Score PrimeScore(DateTime scoreDate, Member member,
            Course course, Tour tour, Member user)
        {
            Score score;
            score = _scoreInfo.getScoreByTourId(tour.TourId, member.MemberId);
            if (score == null)
            {
                score = new Score();
                score.ScoreDate = scoreDate;
                score.MemberId = member.MemberId;
                score.CourseId = course.CourseId;
                score.CreatedTs = DateTime.Now;
                score.EnteredById = user.MemberId;
                score.Color = tour.BaseColor.ToString();
                score.TourEvent = true;
                score.TourId = tour.TourId;
                score.ScoreType = ScoreType.DraftScore;
                _context.Add(score);
                _context.SaveChanges();
            }
            return score;

        }
    }
        
}

