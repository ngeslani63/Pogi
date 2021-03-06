﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Handicap
    {
        public Handicap()
        {
            GhinNumber = 0;
            HcpIndex = 0.0F;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HandicapId { get; set; }
        [Required]
        [Display(Name = "GHIN")]
        public int GhinNumber { get; set; }
        [Display(Name = "Handicap Effective Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Handicap Index")]
        public float HcpIndex { get; set; }
        
    }
}
