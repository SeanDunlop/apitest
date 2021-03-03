using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string name { get; set; }
        //public virtual Device device { get; set; }
    }
}
