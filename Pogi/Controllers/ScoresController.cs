using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pogi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
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
        public ScoresController(PogiDbContext context, IScoreInfo scoreInfo,
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
        // GET: api/<ScoresController>
        [HttpGet]
        public IActionResult Get(string TourId)
        {
            try
            {
                return Ok(_scoreInfo.getForTour(Int32.Parse(TourId)));
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get Scores " + ex.Message.ToString());
            }

        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var score = _scoreInfo.getbyScoreId(id);
                if (score == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(score);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get Scores e " + ex.Message.ToString() );
            }

        }

        // POST api/<ScoresController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ScoresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ScoresInfo model)
        {
            try
            {
                Score Score = _context.Score.SingleOrDefault(m => m.ScoreId == id);
                //Score.CourseId = model.CourseId;
                //Score.Color = model.Color;
                //Score.ScoreDate = model.ScoreDate;
                //Score.EnteredById = model.EnteredBy.MemberId;
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
                Score.Round = model.Round;
                
                int holesPlayed = 0;
                if (Score.Hole01 > 0) holesPlayed++;
                if (Score.Hole02 > 0) holesPlayed++;
                if (Score.Hole03 > 0) holesPlayed++;
                if (Score.Hole04 > 0) holesPlayed++;
                if (Score.Hole05 > 0) holesPlayed++;
                if (Score.Hole06 > 0) holesPlayed++;
                if (Score.Hole07 > 0) holesPlayed++;
                if (Score.Hole08 > 0) holesPlayed++;
                if (Score.Hole09 > 0) holesPlayed++;
                if (Score.Hole10 > 0) holesPlayed++;
                if (Score.Hole11 > 0) holesPlayed++;
                if (Score.Hole12 > 0) holesPlayed++;
                if (Score.Hole13 > 0) holesPlayed++;
                if (Score.Hole14 > 0) holesPlayed++;
                if (Score.Hole15 > 0) holesPlayed++;
                if (Score.Hole16 > 0) holesPlayed++;
                if (Score.Hole17 > 0) holesPlayed++;
                if (Score.Hole18 > 0) holesPlayed++;

                int lastHolePlayed = 0;
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 1, model.Hole01);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 2, model.Hole02);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 3, model.Hole03);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 4, model.Hole04);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 5, model.Hole05);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 6, model.Hole06);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 7, model.Hole07);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 8, model.Hole08);
                lastHolePlayed = setLastHolePlayed(lastHolePlayed, 9, model.Hole09);
                if (lastHolePlayed == 0 || lastHolePlayed >= 9 )
                {
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 10, model.Hole10);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 11, model.Hole11);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 12, model.Hole12);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 13, model.Hole13);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 14, model.Hole14);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 15, model.Hole15);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 16, model.Hole16);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 17, model.Hole17);
                    lastHolePlayed = setLastHolePlayed(lastHolePlayed, 18, model.Hole18);
                }
                Score.LastHolePlayed = lastHolePlayed;

                //Score.TourEvent = model.TourEvent;
                //Score.TourId = model.TourId;
                //if (model.AboutGame == null) model.AboutGame = "";
                //Score.AboutGame = model.AboutGame;

                Score.NetScore = 199;
                Score.TourScore = 199;
                float HcpAllowPct = 100.0F;
                float MultiAdj = 0.0F;
                Tour Tour;


                Handicap Handicap = _handicap.getHandicapForDate(Score.MemberId, Score.ScoreDate);
                CourseDetail CourseDetail = _courseDetail.get(Score.CourseId, Score.Color);
                Course Course = _courseData.get(Score.CourseId);

                if (Score.TourEvent == true && (Tour = _tourInfo.getTour(Score.TourId)) != null)
                {
                    HcpAllowPct = Tour.HcpAllowPct;
                    if (Tour.AllowMultiTee == true)
                    {
                        CourseDetail BaseCourse = _courseDetail.get(Course.CourseId, Tour.BaseColor.ToString());
                        MultiAdj = (float)(BaseCourse.Rating - CourseDetail.Rating);
                    }
                }

                if (Handicap != null && Handicap.HcpIndex > 0)
                {
                    float courseHandicap = Handicap.HcpIndex * CourseDetail.Slope / 113;
                    float courseHandicapT = (Handicap.HcpIndex * CourseDetail.Slope / 113 - MultiAdj) * (HcpAllowPct / 100);
                    Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap*holesPlayed/18.0);
                    Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT * holesPlayed / 18.0);
                }
                else
                {
                    float courseHandicap = getS36Hcp(Score, Course) * CourseDetail.Slope / 113;
                    float courseHandicapT = getS36Hcp(Score, Course) * (HcpAllowPct / 100) * CourseDetail.Slope / 113;
                    Score.NetScore = (int)Math.Round(model.HoleTotal - courseHandicap * holesPlayed / 18.0);
                    Score.TourScore = (int)Math.Round(model.HoleTotal - courseHandicapT * holesPlayed / 18.0);

                }

                countScores(Score, Course);
                Score.Tiebreaker = _scoreInfo.getTiebreaker(Score);

                //Score.LastUpdatedBy = User.Identity.Name;
                Score.LastUpdatedBy = "Live Score";
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
            return Ok();
        }

        // DELETE api/<ScoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
        private int getS36Hcp(Score Score, Course Course)
        {
            int points = 36;
            points = points - getS36Points(Score.Hole01, Course.Par01);
            points = points - getS36Points(Score.Hole02, Course.Par02);
            points = points - getS36Points(Score.Hole03, Course.Par03);
            points = points - getS36Points(Score.Hole04, Course.Par04);
            points = points - getS36Points(Score.Hole05, Course.Par05);
            points = points - getS36Points(Score.Hole06, Course.Par06);
            points = points - getS36Points(Score.Hole07, Course.Par07);
            points = points - getS36Points(Score.Hole08, Course.Par08);
            points = points - getS36Points(Score.Hole09, Course.Par09);
            points = points - getS36Points(Score.Hole10, Course.Par10);
            points = points - getS36Points(Score.Hole11, Course.Par11);
            points = points - getS36Points(Score.Hole12, Course.Par12);
            points = points - getS36Points(Score.Hole13, Course.Par13);
            points = points - getS36Points(Score.Hole14, Course.Par14);
            points = points - getS36Points(Score.Hole15, Course.Par15);
            points = points - getS36Points(Score.Hole16, Course.Par16);
            points = points - getS36Points(Score.Hole17, Course.Par17);
            points = points - getS36Points(Score.Hole18, Course.Par18);
            return points;
        }
        private int getS36Points(int Score, int Par)
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
        private int setLastHolePlayed(int lastHolePlayed, int holeNo, int holeScore)
        {
            if (lastHolePlayed == 0)
            {
                if (holeScore > 0)
                {
                    return holeNo;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (holeScore > 0)
                {
                    return holeNo;
                }
                else
                {
                    return lastHolePlayed;
                }
            }
        }

    }
}
