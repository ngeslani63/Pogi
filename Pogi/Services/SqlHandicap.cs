using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;

namespace Pogi.Services
{
    public class SqlHandicap : IHandicap
    {
        private PogiDbContext _context;

        public SqlHandicap(PogiDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Handicap> getForGhin(int GhinNumber)
        {
            return _context.Handicap.Where(r => r.GhinNumber == GhinNumber).OrderByDescending(r => r.Date);
        }

        public IEnumerable<Handicap> getForMemberId(int MemberId)
        {
            Member Member = (Member)_context.Member.Where(r => r.MemberId == MemberId);
            return _context.Handicap.Where(r => r.GhinNumber == Member.GhinNumber).OrderByDescending(r => r.Date);
        }

        public DateTime getNextDate(int GhinNumber)
        {
            DateTime maxDate = DateTime.Today.Date;
            var Handicap = _context.Handicap.Where(r => r.GhinNumber == GhinNumber).OrderByDescending(r => r.Date).FirstOrDefault();
            if (Handicap != null) maxDate = Handicap.Date.Date;
            HandicapSchedule HandicapSchedule = _context.HandicapSchedule.Where(r => r.Date > maxDate).OrderBy(r => r.Date).FirstOrDefault();
            if (HandicapSchedule != null && HandicapSchedule.Date != null) return HandicapSchedule.Date;
            return maxDate;
        }

        public DateTime getCurrEffDate()
        {
            DateTime currEffDate = DateTime.Today.Date;
            HandicapSchedule HandicapSchedule = _context.HandicapSchedule.Where(r => r.Date <= currEffDate).OrderByDescending(r => r.Date).FirstOrDefault();
            if (HandicapSchedule != null && HandicapSchedule.Date != null) return HandicapSchedule.Date;
            return currEffDate;
        }

        public List<SelectListItem> getActiveDates(String Date)
        {

            IEnumerable<HandicapSchedule> HandicapSchedules = _context.HandicapSchedule.Where(r => r.RecordStatus == RecordState.Active).OrderBy(r => r.Date);
            List<SelectListItem> activeDates = new List<SelectListItem>();
            foreach (var handicapSchedule in HandicapSchedules)
            {
                string text = handicapSchedule.Date.Date.ToShortDateString();
                string value = handicapSchedule.Date.Date.ToShortDateString();
                SelectListItem sl = new SelectListItem { Text = text, Value = value };
                sl.Selected = false;
                if (Date.Equals(value)) { sl.Selected = true; }
                activeDates.Add(sl);
            }
            return activeDates;
        }

        public Handicap getHandicapForDate(int MemberId, DateTime ScoreDate)
        {
            Member Member = (Member)_context.Member.Where(r => r.MemberId == MemberId).FirstOrDefault();
            if (Member.GhinNumber > 0)
            {
                var Handicap = _context.Handicap.Where(r => r.GhinNumber == Member.GhinNumber && r.Date <= ScoreDate).OrderByDescending(r => r.Date).FirstOrDefault();
                return Handicap;
            }
            return null;

        }

        public IEnumerable<HandicapInfo> getAllForDate(DateTime Date)
        {
            IEnumerable<Member> Members = _context.Member.Where(r => r.RecordStatus == RecordState.Active &&
                (r.MemberStatus == MemberState.Member || r.MemberStatus == MemberState.Junior)).OrderBy(r => r.LastName).ThenBy(r => r.FirstName);
            List<HandicapInfo> HandicapInfos = new List<HandicapInfo>();
            foreach (Member Member in Members)
            {
                Handicap Handicap = getHandicapForDate(Member.MemberId, Date);
                if (Handicap != null)
                {
                    HandicapInfo HandicapInfo = new HandicapInfo();
                    HandicapInfo.Member = Member;
                    HandicapInfo.Handicap = Handicap;
                    HandicapInfos.Add(HandicapInfo);
                }
            }
            return HandicapInfos;
        }
        public int getCallawayHcp(Score Score, Course Course)
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
    }
}
