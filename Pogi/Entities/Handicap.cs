using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Handicap
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HandicapId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Display(Name = "GHIN")]
        public int GhinNumber { get; set; }
        public DateTime Date { get; set; }
        float HcpIndex { get; set; }
        
    }
}
