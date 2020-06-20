﻿using System;
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
            _tourInfo = tourInfo;
            _tourDayInfo = tourDayInfo;
            _teeTimeInfo = teeTimeInfo;
            _httpContextAccessor = httpContextAccessor;
            _activity = activity;
            _dateTime = dateTime;
        }

        public IActionResult Index(string TourId, string TourDate, string memberId, string tGroup,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4)
        {
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
                return RedirectToAction("Index", "Live", new { TourDate = TourDate, TourId = TourId });
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
            model.Tours = _tourInfo.getTours(false, true);
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
            _activity.logActivity(userName, "Live");
            return View(model);
        }
        public IActionResult Score(string TourId, string TourDate, string memberId, string tGroup,
            string sMemberId1, string sMemberId2, string sMemberId3, string sMemberId4)
        {
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
            if (tour == null)
            {
                return RedirectToAction("Index", "Live");
            }
            string[] sPlayer = { sMemberId1, sMemberId2, sMemberId3, sMemberId4 };
            int[] player = { 0, 0, 0, 0 };
            int pCnt = 0;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    int mId = int.Parse(sPlayer[i]);
                    if (mId > 0)
                    {
                        pCnt++;
                        player[pCnt - 1] = mId;
                    }
                }
                catch (ArgumentException ex)
                {

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
            for (int i = 0; i < pCnt; i++)
            {
                Member member = _memberData.get(player[i]);

                model.Players.Add(member);
                Score score = PrimeScore(DateTime.Parse(TourDate, CultureInfo.CurrentCulture),
                               member, model.Course, tour, user);
                model.Scores.Add(score) ;
            }
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

