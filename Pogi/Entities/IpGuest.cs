using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Entities
{
    public class IpGuest
    {
        public IpGuest()
        {
            UserName = "";
            
        }
        [Key]
        public string IpAddr { get; set; }
        public string UserName { get; set; }
        public DateTime LastUpdtTs { get; set; }
    }
}
