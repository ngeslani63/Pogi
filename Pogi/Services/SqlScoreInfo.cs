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
        private ITourInfo _tourInfo;

        public SqlScoreInfo(PogiDbContext context, ITourInfo tourInfo)
        {
            _context = context;
            _tourInfo = tourInfo;
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
        public List<ScoreInfo> getForTour(string TourName)
        {
            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();
            Tour Tour = _context.Tour.FirstOrDefault(r => r.TourName.Contains(TourName));
            if (Tour != null)
            {
                var Scores = _context.Score.Where(r => r.TourId == Tour.TourId).OrderByDescending(r => r.ScoreDate).ThenBy(i => i.ScoreId);


                foreach (Score score in Scores)
                {
                    Member member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                    Course course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);

                    ScoreInfo scoreInfo = new ScoreInfo(member, course, score);

                    ScoreInfos.Add(scoreInfo);
                }
            }
            return ScoreInfos;
        }
        public List<ScoreInfo> getForTour(int TourId)
        {
            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();
            if (TourId > 0)
            {
                Tour Tour = _tourInfo.getTour(TourId);
                var TourType = Tour.TourType;
                if (TourType == TourType.SingleDay)
                {
                    var Scores = _context.Score.Where(r => r.TourEvent && r.TourId == TourId).OrderByDescending(r => r.ScoreDate).ThenBy(i => i.ScoreId);
                    foreach (Score score in Scores)
                    {
                        Member member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                        Course course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);

                        ScoreInfo scoreInfo = new ScoreInfo(member, course, score);

                        ScoreInfos.Add(scoreInfo);
                    }
                }
                else
                {
                    var TourDays = _context.TourDay.Where(r => r.TourId == Tour.TourId).OrderByDescending(r => r.TourDate);
                    foreach (TourDay TourDay in TourDays)
                    {
                        var Scores = _context.Score.Where(r => r.TourEvent && r.ScoreDate.Date == TourDay.TourDate);
                        foreach (Score score in Scores)
                        {
                            Member member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                            Course course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);

                            ScoreInfo scoreInfo = new ScoreInfo(member, course, score);

                            ScoreInfos.Add(scoreInfo);
                        }
                    }
                }



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
            return getMeritsByDate("Month " + theYear.ToString() + "/" + theMonth.ToString() + " ", startDate, endDate);
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
            DateTime endDate = today.AddDays(daysSinceSunday); // Sunday 12:00 AM
            return getBadgesByDate("Weekly ", startDate, endDate);
        }
        public List<ScoreInfo> getBadgesOfWeek(DateTime date)
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

            return getBadgesByDate("Weekly ", startDate, endDate);
        }
        public List<ScoreInfo> getBadgesOfMonth(DateTime date)
        {
            int theYear = date.Year;
            int theMonth = date.Month;
            DateTime startDate = new DateTime(theYear, theMonth, 01).Date;
            DateTime endDate = new DateTime(theYear, theMonth + 1, 01).Date.AddMinutes(-1);
            return getBadgesByDate("Month " + theYear.ToString() + "/" + theMonth.ToString() + " ", startDate, endDate);
        }
        public List<ScoreInfo> getBadgesOfYear(DateTime date)
        {
            int theYear = date.Year;
            DateTime startDate = new DateTime(theYear, 01, 01).Date;
            DateTime endDate = new DateTime(theYear, 12, 31).Date.AddHours(23).AddMinutes(59);
            return getBadgesByDate(theYear.ToString() + " ", startDate, endDate);
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
                .OrderByDescending(r => r.Pars + r.Birdies + r.Eagles + r.Albatross).ThenBy(r => r.NetScore).ToList();
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

        public string getTiebreaker(Score Score)
        {
            Member member = _context.Member.SingleOrDefault(r => r.MemberId == Score.MemberId);
            CourseHandicap courseHandicap = _context.CourseHandicap.SingleOrDefault(r => r.CourseId == Score.CourseId);
            byte[] scores = new byte[18];
            scores[0] = (byte)Score.Hole01;
            scores[1] = (byte)Score.Hole02;
            scores[2] = (byte)Score.Hole03;
            scores[3] = (byte)Score.Hole04;
            scores[4] = (byte)Score.Hole05;
            scores[5] = (byte)Score.Hole06;
            scores[6] = (byte)Score.Hole07;
            scores[7] = (byte)Score.Hole08;
            scores[8] = (byte)Score.Hole09;
            scores[9] = (byte)Score.Hole10;
            scores[10] = (byte)Score.Hole11;
            scores[11] = (byte)Score.Hole12;
            scores[12] = (byte)Score.Hole13;
            scores[13] = (byte)Score.Hole14;
            scores[14] = (byte)Score.Hole15;
            scores[15] = (byte)Score.Hole16;
            scores[16] = (byte)Score.Hole17;
            scores[17] = (byte)Score.Hole18;
            byte[] cHcps = new byte[18];
            if (member.Gender == GenderType.Male)
            {
                cHcps[0] = (byte)courseHandicap.MenHcp01;
                cHcps[1] = (byte)courseHandicap.MenHcp02;
                cHcps[2] = (byte)courseHandicap.MenHcp03;
                cHcps[3] = (byte)courseHandicap.MenHcp04;
                cHcps[4] = (byte)courseHandicap.MenHcp05;
                cHcps[5] = (byte)courseHandicap.MenHcp06;
                cHcps[6] = (byte)courseHandicap.MenHcp07;
                cHcps[7] = (byte)courseHandicap.MenHcp08;
                cHcps[8] = (byte)courseHandicap.MenHcp09;
                cHcps[9] = (byte)courseHandicap.MenHcp10;
                cHcps[10] = (byte)courseHandicap.MenHcp11;
                cHcps[11] = (byte)courseHandicap.MenHcp12;
                cHcps[12] = (byte)courseHandicap.MenHcp13;
                cHcps[13] = (byte)courseHandicap.MenHcp14;
                cHcps[14] = (byte)courseHandicap.MenHcp15;
                cHcps[15] = (byte)courseHandicap.MenHcp16;
                cHcps[16] = (byte)courseHandicap.MenHcp17;
                cHcps[17] = (byte)courseHandicap.MenHcp18;
            }
            else
            {
                cHcps[0] = (byte)courseHandicap.LadiesHcp01;
                cHcps[1] = (byte)courseHandicap.LadiesHcp02;
                cHcps[2] = (byte)courseHandicap.LadiesHcp03;
                cHcps[3] = (byte)courseHandicap.LadiesHcp04;
                cHcps[4] = (byte)courseHandicap.LadiesHcp05;
                cHcps[5] = (byte)courseHandicap.LadiesHcp06;
                cHcps[6] = (byte)courseHandicap.LadiesHcp07;
                cHcps[7] = (byte)courseHandicap.LadiesHcp08;
                cHcps[8] = (byte)courseHandicap.LadiesHcp09;
                cHcps[9] = (byte)courseHandicap.LadiesHcp10;
                cHcps[10] = (byte)courseHandicap.LadiesHcp11;
                cHcps[11] = (byte)courseHandicap.LadiesHcp12;
                cHcps[12] = (byte)courseHandicap.LadiesHcp13;
                cHcps[13] = (byte)courseHandicap.LadiesHcp14;
                cHcps[14] = (byte)courseHandicap.LadiesHcp15;
                cHcps[15] = (byte)courseHandicap.LadiesHcp16;
                cHcps[16] = (byte)courseHandicap.LadiesHcp17;
                cHcps[17] = (byte)courseHandicap.LadiesHcp18;
            }
        }
        private void pushScore(byte[] scores, byte[] ties, int i)
        {

        }
    }
}
