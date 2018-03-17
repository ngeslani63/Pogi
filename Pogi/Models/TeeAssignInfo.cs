using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models
{
    public class TeeAssignInfo
    {
        public TeeAssignInfo(TeeAssign teeAssign, TeeTime teeTime, Member member, Course course)
        {
            TeeAsign= teeAssign;
            TeeTime = teeTime;
            Member = member;
            Course = course;

        }
        public TeeAssign TeeAsign { get; set; }
        public TeeTime TeeTime { get; set; }
        public Member Member { get; set; }
        public Course Course { get; set; }
    }
}
