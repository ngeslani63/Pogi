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
        public List<ScoreInfo> getMerits()
        {
            var Scores = _context.Score.OrderBy(r => r.ScoreDate).ThenBy(i => i.ScoreId);

            List<ScoreInfo> ScoreInfos = new List<ScoreInfo>();

            var scores = _context.Score.OrderBy(r => r.HoleTotal).ThenBy(r => r.NetScore).ToList();
            var score = scores[0];
            Member member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
            Course course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
            String merit = "Low Gross (1st)";
            ScoreInfo scoreInfo = new ScoreInfo(member, course, score, merit);
            ScoreInfos.Add(scoreInfo);

            score = scores[1];
            if (score != null)
            {
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = "Low Gross (2nd)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
            }

            score = scores[2];
            if (score != null)
            {
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = "Low Gross (3rd)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
            }

            scores = _context.Score.OrderBy(r => r.NetScore).ThenBy(r => r.HoleTotal).ToList();
            score = scores[0];
            if (score != null)
            {
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = "Low Net (1st)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
            }
            score = scores[1];
            if (score != null)
            {
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = "Low Net (2nd)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
            }
            score = scores[2];
            if (score != null)
            {
                member = _context.Member.FirstOrDefault(r => r.MemberId == score.MemberId);
                course = _context.Course.FirstOrDefault(r => r.CourseId == score.CourseId);
                merit = "Low Net (3rd)";
                scoreInfo = new ScoreInfo(member, course, score, merit);
                ScoreInfos.Add(scoreInfo);
            }
            return ScoreInfos;
        }
    }
}
