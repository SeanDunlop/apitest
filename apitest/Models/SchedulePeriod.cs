using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class SchedulePeriod
    {
        public int SchedulePeriodId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int intensity { get; set; }

    }
}
