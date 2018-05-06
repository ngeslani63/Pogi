using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;

namespace Pogi.Services
{
    public class SqlScoreInfo : IScoreInfo
    {
        private PogiDbContext _context;

        public SqlScoreInfo(PogiDbContext context)
        {
            _context = context;
        }
        public List<ScoreInfo> getAll()
        {
            var Scores = _context.Score.OrderByDescending(r => r.ScoreDate).ThenBy(i => i.ScoreId);

            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();
            foreach (Score score in Scores)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                Course course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);

                ScoreInfo scoreInfo = new ScoreInfo(member, course, score);

                ScoreInfos.Add(scoreInfo);
            }
            return ScoreInfos;
        }
        public List<ScoreInfo> getMeritsAllTime()
        {
            DateTime startDate = new DateTime(2018, 01, 01).Date;
            DateTime endDate = new DateTime(9999, 12, 31).Date;
            return getMeritsByDate("Overall ", startDate, endDate);
        }

        public List<ScoreInfo> getMeritsLastWeek()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysSinceSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek - 7) % 7;
            DateTime startDate = today.AddDays(daysSinceSunday - 7); // Sunday of Last Week
            DateTime endDate = today.AddDays(daysSinceSunday); // Sunday 12:00 AM
            return getMeritsByDate("Weekly ", startDate, endDate);
        }
        public List<ScoreInfo> getMeritsOfWeek(DateTime date)
        {
            // Initialize date to 0 hour
            date = date.Date;
            DateTime startDate;
            DateTime endDate;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysSinceSunday = ((int)DayOfWeek.Sunday - (int)date.DayOfWeek - 7) % 7;
            if (daysSinceSunday == 0)
            {
                startDate = date.AddDays(-6); // Monday of week, 0 hour (where week = Monday to Sunday);
            }
            else
            {
                startDate = date.AddDays(daysSinceSunday + 1);  // Monday, 0 hour
            }
            endDate = startDate.AddDays(6).AddHours(23).AddMinutes(59);

            return getMeritsByDate("Weekly ", startDate, endDate);
        }
        public List<ScoreInfo> getMeritsOfMonth(DateTime date)
        {
            int theYear = date.Year;
            int theMonth = date.Month;
            DateTime startDate = new DateTime(theYear, theMonth, 01).Date;
            DateTime endDate = new DateTime(theYear, theMonth + 1, 01).Date.AddMinutes(-1);
            return getMeritsByDate("Month " + theYear.ToString() + "/" + theMonth.ToString() + " " , startDate, endDate);
        }

        public List<ScoreInfo> getMeritsOfYear(DateTime date)
        {
            int theYear = date.Year;
            DateTime startDate = new DateTime(theYear, 01, 01).Date;
            DateTime endDate = new DateTime(theYear, 12, 31).Date.AddHours(23).AddMinutes(59);
            return getMeritsByDate(theYear.ToString() + " ", startDate, endDate);
        }
        public List<ScoreInfo> getMeritsThisYear()
        {
            DateTime today = DateTime.Today;
            int thisYear = today.Year;
            DateTime startDate = new DateTime(thisYear, 01, 01).Date;
            DateTime endDate = new DateTime(thisYear, 12, 31).Date;
            return getMeritsByDate(thisYear.ToString() + " ", startDate, endDate);
        }


        public List<ScoreInfo> getMeritsByDate(String timeFrame, DateTime startDate, DateTime endDate)
        {
            //var Scores = _context.Score.Where(r => r.ScoreDate >= startDate && r.ScoreDate <= endDate)
            //    .OrderBy(r => r.ScoreDate).ThenBy(i => i.ScoreId);

            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();

            var scores = _context.Score.Where(r => r.ScoreDate >= startDate && r.ScoreDate <= endDate)
                .OrderBy(r => r.HoleTotal).ThenBy(r => r.NetScore).ToList();
            Score score;
            Member member;
            Course course;
            String merit;
            ScoreInfo scoreInfo;
            if (scores.Count > 0)
            {
                score = scores[0];
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = timeFrame + "Low Gross (1st)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
                if (scores.Count > 1)
                {
                    score = scores[1];
                    if (score != null)
                    {
                        member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                        course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                        merit = timeFrame + "Low Gross (2nd)";
                        scoreInfo = new ScoreInfo(member, course, score, merit);
                        ScoreInfos.Add(scoreInfo);
                    }
                    if (scores.Count > 2)
                    {
                        score = scores[2];
                        if (score != null)
                        {
                            member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                            course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                            merit = timeFrame + "Low Gross (3rd)";
                            scoreInfo = new ScoreInfo(member, course, score, merit);
                            ScoreInfos.Add(scoreInfo);
                        }
                    }
                }
            }

            scores = _context.Score.Where(r => r.ScoreDate >= startDate && r.ScoreDate <= endDate)
                .OrderBy(r => r.NetScore).ThenBy(r => r.HoleTotal).ToList();
            
            if (scores.Count > 0)
            {
                score = scores[0];
                if (score != null)
                {
                    member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                    course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                    merit = timeFrame + "Low Net (1st)";
                    scoreInfo = new ScoreInfo(member, course, score, merit);
                    ScoreInfos.Add(scoreInfo);
                }
                if (scores.Count > 1)
                {
                    score = scores[1];
                    if (score != null)
                    {
                        member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                        course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                        merit = timeFrame + "Low Net (2nd)";
                        scoreInfo = new ScoreInfo(member, course, score, merit);
                        ScoreInfos.Add(scoreInfo);
                    }
                    if (scores.Count > 2)
                    {
                        score = scores[2];
                        if (score != null)
                        {
                            member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                            course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                            merit = timeFrame + "Low Net (3rd)";
                            scoreInfo = new ScoreInfo(member, course, score, merit);
                            ScoreInfos.Add(scoreInfo);
                        }
                    }
                }
            }
            return ScoreInfos;
        }
        public List<ScoreInfo> getBadgesAllTime()
        {
            DateTime startDate = new DateTime(2018, 01, 01).Date;
            DateTime endDate = new DateTime(9999, 12, 31).Date;
            return getBadgesByDate("Overall ", startDate, endDate);
        }

        public List<ScoreInfo> getBadgesLastWeek()
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysSinceSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek - 7) % 7;
            DateTime startDate = today.AddDays(daysSinceSunday - 7); // Sunday of Last Week
            DateTime endDate = today.AddDays(daysSinceSunday ); // Sunday 12:00 AM
            return getBadgesByDate("Weekly ", startDate, endDate);
        }
        public List<ScoreInfo> getBadgesThisYear()
        {
            DateTime today = DateTime.Today;
            int thisYear = today.Year;
            DateTime startDate = new DateTime(thisYear, 01, 01).Date;
            DateTime endDate = new DateTime(thisYear, 12, 31).Date;
            return getBadgesByDate(thisYear.ToString() + " ", startDate, endDate);
        }
        public List<ScoreInfo> getBadgesByDate(String timeFrame, DateTime startDate, DateTime endDate)
        {
            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();

            var scores = _context.Score.Where(r => r.ScoreDate >= startDate && r.ScoreDate <= endDate)
                .OrderByDescending(r => r.Pars + r.Birdies+ r.Eagles + r.Albatross).ThenBy(r => r.NetScore).ToList();
            Score score;
            Member member;
            Course course;
            String merit;
            ScoreInfo scoreInfo;
            if (scores.Count > 0)
            {
                score = scores[0];
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = timeFrame + "Most Pars or Better (1st)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
                if (scores.Count > 1)
                {
                    score = scores[1];
                    if (score != null)
                    {
                        member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                        course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                        merit = timeFrame + "Most Pars or Better (2nd)";
                        scoreInfo = new ScoreInfo(member, course, score, merit);
                        ScoreInfos.Add(scoreInfo);
                    }
                    if (scores.Count > 2)
                    {
                        score = scores[2];
                        if (score != null)
                        {
                            member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                            course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                            merit = timeFrame + "Most Pars or Better (3rd)";
                            scoreInfo = new ScoreInfo(member, course, score, merit);
                            ScoreInfos.Add(scoreInfo);
                        }
                    }
                }
            }

            scores = _context.Score.Where(r => r.ScoreDate >= startDate && r.ScoreDate <= endDate)
                .OrderByDescending(r => r.Birdies + r.Eagles + r.Albatross).ThenBy(r => r.NetScore).ToList();
            
            if (scores.Count > 0)
            {
                score = scores[0];
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = timeFrame + "Most Birdies or Better (1st)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
                if (scores.Count > 1)
                {
                    score = scores[1];
                    if (score != null)
                    {
                        member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                        course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                        merit = timeFrame + "Most Birdies or Better (2nd)";
                        scoreInfo = new ScoreInfo(member, course, score, merit);
                        ScoreInfos.Add(scoreInfo);
                    }
                    if (scores.Count > 2)
                    {
                        score = scores[2];
                        if (score != null)
                        {
                            member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                            course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                            merit = timeFrame + "Most Birdies or Better (3rd)";
                            scoreInfo = new ScoreInfo(member, course, score, merit);
                            ScoreInfos.Add(scoreInfo);
                        }
                    }
                }
            }
            return ScoreInfos;
        }
    }
}
