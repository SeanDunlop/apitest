using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Session
    {
        
        public int SessionId { get; set; }

        public int UserId { get; set; }
        public string SessionGuid { get; set; }
        public bool Active { get; set; }
        public DateTime Timeout { get; set; }
    }
}
