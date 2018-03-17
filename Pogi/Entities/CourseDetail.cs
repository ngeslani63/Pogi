using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class CourseDetail
    {
        public CourseDetail()
        {
            Rating = 0.0F;
            Color = "";
            Yards01 = 0;
            Yards02 = 0;
            Yards03 = 0;
            Yards04 = 0;
            Yards05 = 0;
            Yards06 = 0;
            Yards07 = 0;
            Yards08 = 0;
            Yards09 = 0;
            Yards10 = 0;
            Yards11 = 0;
            Yards12 = 0;
            Yards13 = 0;
            Yards14 = 0;
            Yards15 = 0;
            Yards16 = 0;
            Yards17 = 0;
            Yards18 = 0;
            YardsIn = 0;
            YardsOut = 0;
            YardsTotal = 0;

        }
        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Color { get; set; }
        public int Yards01 { get; set; }
        public int Yards02 { get; set; }
        public int Yards03 { get; set; }
        public int Yards04 { get; set; }
        public int Yards05 { get; set; }
        public int Yards06 { get; set; }
        public int Yards07 { get; set; }

        public int Yards08 { get; set; }
        public int Yards09 { get; set; }
        public int Yards10 { get; set; }
        public int Yards11 { get; set; }
        public int Yards12 { get; set; }
        public int Yards13 { get; set; }
        public int Yards14 { get; set; }

        public int Yards15 { get; set; }
        public int Yards16 { get; set; }
        public int Yards17 { get; set; }
        public int Yards18 { get; set; }
        public int YardsIn { get; set; }
        public int YardsOut { get; set; }
        public int YardsTotal { get; set; }
        public float Rating { get; set; }

        public int Slope { get; set; }
    }

}
