using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pogi.Entities
{
    public class TourPlayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourPlayerId { get; set; }
        public int TourId { get; set; }
        public int MemberId { get; set; }
    }
}
