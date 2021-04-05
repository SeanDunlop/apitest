using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Session
    {
        
        public int SessionId { get; set; }

        public String SessionGuid { get; set; }
        public DateTime Timeout { get; set; }
    }
}
