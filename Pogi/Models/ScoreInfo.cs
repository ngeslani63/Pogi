using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models
{
    public class ScoreInfo
    {
        public ScoreInfo(Member member, Course course, Score score)
        {
            
            Member = member;
            Course = course;
            Score = score;
        }
        public Score Score { get; set; }
        public Member Member { get; set; }
        public Course Course { get; set; }
         }
}

