using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.TeeTimeViewModels
{
    public class TeeTimeInfo
    {
        public TeeTimeInfo(TeeTime teeTime, Member member, Course course, List<TeeAssignInfo> teeAssignInfos)
        {
            TeeTime = teeTime;
            Member = member;
            Course = course;
            TeeAssignInfos = teeAssignInfos;
        }
        public TeeTime TeeTime { get; set; }
        public Member Member { get; set; }
        public Course Course{ get; set; }
        public List<TeeAssignInfo> TeeAssignInfos { get; set; }
    }
}
