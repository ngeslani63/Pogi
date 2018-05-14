using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class Activity
    {
        public Activity()
        {
            MemberId = 0;
            UserName = "Guest";
            ipAddr = "";
            Action = "";
            createdTS = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        public int MemberId { get; set; }
        public string UserName { get; set; }
        public string ipAddr { get; set; }
        [Required]
        public string Action { get; set; }
        public DateTime createdTS { get; set; }

    }
}
