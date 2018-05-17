using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;

namespace Pogi.Services
{

    public class SqlTeeAssignInfo : ITeeAssignInfo
    {
        private PogiDbContext _context;

        public SqlTeeAssignInfo(PogiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TeeAssignInfo> getAll()
        {
            var TeeAssigns = _context.TeeAssign.Where(r => r.RecordStatus == RecordState.Active).OrderBy(r => r.Group).ThenBy(r => r.Order).ThenBy(r => r.TeeTimeId);
            List<TeeAssignInfo> TeeAssignInfos = new List<TeeAssignInfo>();
            foreach (TeeAssign teeAssign in TeeAssigns)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == teeAssign.MemberId);
                TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeId == teeAssign.TeeTimeId);
                Course course = _context.Course.FirstOrDefault(r => r.CourseId == teeTime.CourseId);

                TeeAssignInfo teeAssignInfo = new TeeAssignInfo(teeAssign, teeTime, member, course);
                TeeAssignInfos.Add(teeAssignInfo);
            }
            return TeeAssignInfos;
        }

        public List<TeeAssignInfo> getAllForTeeTime(int teeTimeId)
        {
            return getForTeeTimeCommon(teeTimeId, true);
        }
        public List<TeeAssignInfo> getForTeeTime(int teeTimeId)
        {
            return getForTeeTimeCommon(teeTimeId, false);
        }

        List<TeeAssignInfo> getForTeeTimeCommon(int teeTimeId, bool max)
        {
            TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeId == teeTimeId);
            Course course = _context.Course.FirstOrDefault(r => r.CourseId == teeTime.CourseId);

            var TeeAssigns = _context.TeeAssign.Where(r => r.TeeTimeId == teeTimeId).OrderBy(r => r.Group).ThenBy(r => r.Order).ThenBy(r => r.TeeAssignId);
            List<TeeAssignInfo> TeeAssignInfos = new List<TeeAssignInfo>();
            foreach (TeeAssign teeAssign in TeeAssigns)
            {
                Member member = _context.Member.FirstOrDefault(r => r.MemberId == teeAssign.MemberId);
                //TeeTime teeTime = _context.TeeTime.FirstOrDefault(r => r.TeeTimeId == teeAssign.TeeTimeId);
                //Course course = _context.Course.FirstOrDefault(r => r.CourseId == teeTime.CourseId);
                TeeAssignInfo teeAssignInfo = new TeeAssignInfo(teeAssign, teeTime, member, course);
                TeeAssignInfos.Add(teeAssignInfo);
            }
            if (max == true)
            {
                while (TeeAssignInfos.Count < teeTime.NumPlayers)
                {
                    TeeAssign teeAssign = new TeeAssign();
                    teeAssign.TeeTimeId = teeTimeId;
                    Member member = new Member();
                    TeeAssignInfo teeAssignInfo = new TeeAssignInfo(teeAssign, teeTime, member, course);
                    TeeAssignInfos.Add(teeAssignInfo);
                }

            }
            return TeeAssignInfos;
        }
    }
}
