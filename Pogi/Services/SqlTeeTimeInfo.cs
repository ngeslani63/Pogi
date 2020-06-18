using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Models.TeeTimeViewModels;

namespace Pogi.Services
{
    public class SqlTeeTimeInfo : ITeeTimeInfo
    {
        private PogiDbContext _context;
		private ITeeAssignInfo _teeAssignInfo;
        private IDateTime _dateTime;

        public SqlTeeTimeInfo(PogiDbContext context, ITeeAssignInfo teeAssignInfo, IDateTime dateTime)
        {
            _context = context;
			_teeAssignInfo = teeAssignInfo;
            _dateTime = dateTime;
        }
        public IEnumerable<TeeTimeInfo> getAll()
        {
            var TeeTimes =  _context.TeeTime.Where(r => r.TeeTimeTS >= _dateTime.getToday()).OrderBy(r => r.TeeTimeTS);
			
            List<TeeTimeInfo> TeeTimeInfos = new List<TeeTimeInfo>();
            foreach(TeeTime teeTime in TeeTimes)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == teeTime.ReservedById);
                Course course = _context.Course.FirstOrDefault(r => r.CourseId == teeTime.CourseId);
				List<TeeAssignInfo> teeAssignInfos =_teeAssignInfo.getForTeeTime(teeTime.TeeTimeId);
	            TeeTimeInfo teeTimeInfo= new TeeTimeInfo(teeTime, member,course,teeAssignInfos);

                TeeTimeInfos.Add(teeTimeInfo);
            }
            return TeeTimeInfos;
        }
        public TeeTimeInfo getTeeTimeInfo(TeeTime teeTime)
        {
            Member member = _context.Member.FirstOrDefault(r => r.MemberId == teeTime.ReservedById);
            Course course = _context.Course.FirstOrDefault(r => r.CourseId == teeTime.CourseId);
            List<TeeAssignInfo> teeAssignInfos = _teeAssignInfo.getForTeeTime(teeTime.TeeTimeId);
            TeeTimeInfo teeTimeInfo = new TeeTimeInfo(teeTime, member, course, teeAssignInfos);
                   
            return teeTimeInfo;
        }

        public TeeTime GetTeeTime(DateTime dateTime)
        {
            DateTime date = dateTime.Date;
            DateTime datePlus1 = date.AddDays(1);
            TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeTS >= date
                && r.TeeTimeTS < datePlus1);
            return teeTime;
        }
        public TeeTime GetMajorTeeTime(DateTime dateTime)
        {
            DateTime date = dateTime.Date;
            DateTime datePlus1 = date.AddDays(1);
            TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeTS >= date
                && r.TeeTimeTS < datePlus1 && r.MajorTour == true);
            return teeTime;
        }

        public bool majorTourDay(DateTime dateTime)
        {
            bool rc = false;
            DateTime date = dateTime.Date;
            DateTime datePlus1 = date.AddDays(1);
            TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeTS >= date 
				&& r.TeeTimeTS < datePlus1
				&& r.MajorTour == true);
            if (teeTime != null ) rc = true;

            return rc;
        }
        public int getLockWithdrawDays(DateTime dateTime)
        {
            int days = 5;
            DateTime date = dateTime.Date;
            DateTime datePlus1 = date.AddDays(1);
            TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeTS >= date
                && r.TeeTimeTS < datePlus1
                && r.MajorTour == true);
            if (teeTime != null) days = teeTime.LockWithdrawDays;
            return days;
        }

       
    }
}
