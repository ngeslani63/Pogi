using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            
            TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate,
                CultureInfo.CurrentCulture));
            model.Course = _courseData.get(teeTime.CourseId);
            model.Hole = (short)tHole;
            CourseMap courseMap = _courseMap.get(teeTime.CourseId);
            if (courseMap != null)
            {
                model.Zoom = courseMap.Zoom;

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
                switch (tHole)
                {
                    case 01:
                        model.Lat = courseMap.Lat01;
                        model.Lng = courseMap.Lng01;
                        model.Hdg = courseMap.Hdg01;
                        break;
                    case 02:
                        model.Lat = courseMap.Lat02;
                        model.Lng = courseMap.Lng02;
                        model.Hdg = courseMap.Hdg02;
                        break;
                    case 03:
                        model.Lat = courseMap.Lat03;
                        model.Lng = courseMap.Lng03;
                        model.Hdg = courseMap.Hdg03;
                        break;
                    case 04:
                        model.Lat = courseMap.Lat04;
                        model.Lng = courseMap.Lng04;
                        model.Hdg = courseMap.Hdg04;
                        break;
                    case 05:
                        model.Lat = courseMap.Lat05;
                        model.Lng = courseMap.Lng05;
                        model.Hdg = courseMap.Hdg05;
                        break;
                    case 06:
                        model.Lat = courseMap.Lat06;
                        model.Lng = courseMap.Lng06;
                        model.Hdg = courseMap.Hdg06;
                        break;
                    case 07:
                        model.Lat = courseMap.Lat07;
                        model.Lng = courseMap.Lng07;
                        model.Hdg = courseMap.Hdg07;
                        break;
                    case 08:
                        model.Lat = courseMap.Lat08;
                        model.Lng = courseMap.Lng08;
                        model.Hdg = courseMap.Hdg08;
                        break;
                    case 09:
                        model.Lat = courseMap.Lat09;
                        model.Lng = courseMap.Lng09;
                        model.Hdg = courseMap.Hdg09;
                        break;
                    case 10:
                        model.Lat = courseMap.Lat10;
                        model.Lng = courseMap.Lng10;
                        model.Hdg = courseMap.Hdg10;
                        break;
                    case 11:
                        model.Lat = courseMap.Lat11;
                        model.Lng = courseMap.Lng11;
                        model.Hdg = courseMap.Hdg11;
                        break;
                    case 12:
                        model.Lat = courseMap.Lat12;
                        model.Lng = courseMap.Lng12;
                        model.Hdg = courseMap.Hdg12;
                        break;
                    case 13:
                        model.Lat = courseMap.Lat13;
                        model.Lng = courseMap.Lng13;
                        model.Hdg = courseMap.Hdg13;
                        break;
                    case 14:
                        model.Lat = courseMap.Lat14;
                        model.Lng = courseMap.Lng14;
                        model.Hdg = courseMap.Hdg14;
                        break;
                    case 15:
                        model.Lat = courseMap.Lat15;
                        model.Lng = courseMap.Lng15;
                        model.Hdg = courseMap.Hdg15;
                        break;
                    case 16:
                        model.Lat = courseMap.Lat16;
                        model.Lng = courseMap.Lng16;
                        model.Hdg = courseMap.Hdg16;
                        break;
                    case 17:
                        model.Lat = courseMap.Lat17;
                        model.Lng = courseMap.Lng17;
                        model.Hdg = courseMap.Hdg17;
                        break;
                    case 18:
                        model.Lat = courseMap.Lat18;
                        model.Lng = courseMap.Lng18;
                        model.Hdg = courseMap.Hdg18;
                        break;
                    default:
                        model.Lat = courseMap.Lat01;
                        model.Lng = courseMap.Lng01;
                        model.Hdg = courseMap.Hdg01;
                        break;
                }
            }
            else
            {
                courseMap = new CourseMap();
                model.Zoom = courseMap.Zoom;
                model.Lat = courseMap.Lat01;    
                model.Lng = courseMap.Lng01;
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
                TeeTime teeTime = _teeTimeInfo.GetMajorTeeTime(DateTime.Parse(TourDate).Date);
                if (teeTime != null)
                {
                    model.TeeTimeInfo = _teeTimeInfo.getTeeTimeInfo(teeTime);
                }
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
            string userName = "";
            if (_signInManager.IsSignedIn(User))
            {
                model.User = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                if (model.User != null) userName = model.User.EmailAddr1st;
            }
            _activity.logActivity(userName, "Live Select Group");
            return View(model);
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

