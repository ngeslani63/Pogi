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
        public int Rating { get; set; }

        public int Slope { get; set; }
    }

}
