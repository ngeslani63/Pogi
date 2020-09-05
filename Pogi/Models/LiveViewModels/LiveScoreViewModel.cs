using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.LiveViewModels
{
    public class LiveScoreViewModel
    {
        public LiveScoreViewModel()
        {
            Member User = new Member();
            Colors = new List<SelectListItem>();
            Players = new List<Member>();
            Scores = new List<Score>();
        }
        public Member User { get; set; }
        public Tour Tour { get; set; }
        public TeeTime TeeTime { get; set; }
        public Course Course { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<Score> Scores { get; set; }
        public List<Member> Players { get; set; }
        public string currPlayer { get; set; }
        public string nextHole { get; set; }
        public int[] rankedHoles { get; set; }

    }
}
