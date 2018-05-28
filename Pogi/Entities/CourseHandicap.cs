using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{

    public class CourseHandicap
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public int MenHcp01 { get; set; }
        public int MenHcp02 { get; set; }
        public int MenHcp03 { get; set; }
        public int MenHcp04 { get; set; }
        public int MenHcp05 { get; set; }
        public int MenHcp06 { get; set; }
        public int MenHcp07 { get; set; }
        public int MenHcp08 { get; set; }
        public int MenHcp09 { get; set; }
        public int MenHcp10 { get; set; }
        public int MenHcp11 { get; set; }
        public int MenHcp12{ get; set; }
        public int MenHcp13 { get; set; }
        public int MenHcp14 { get; set; }
        public int MenHcp15 { get; set; }
        public int MenHcp16 { get; set; }
        public int MenHcp17 { get; set; }
        public int MenHcp18 { get; set; }
        public int LadiesHcp01 { get; set; }
        public int LadiesHcp02 { get; set; }
        public int LadiesHcp03 { get; set; }
        public int LadiesHcp04 { get; set; }
        public int LadiesHcp05 { get; set; }
        public int LadiesHcp06 { get; set; }
        public int LadiesHcp07 { get; set; }
        public int LadiesHcp08 { get; set; }
        public int LadiesHcp09 { get; set; }
        public int LadiesHcp10 { get; set; }
        public int LadiesHcp11 { get; set; }
        public int LadiesHcp12 { get; set; }
        public int LadiesHcp13 { get; set; }
        public int LadiesHcp14 { get; set; }
        public int LadiesHcp15 { get; set; }
        public int LadiesHcp16 { get; set; }
        public int LadiesHcp17 { get; set; }
        public int LadiesHcp18 { get; set; }

    }
}
