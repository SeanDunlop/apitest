using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; } // id for the table
        //public int startTime { get; set; } // ignore this in future
        //public int endTime { get; set; } // ignore this in future
        public string name { get; set; } // name of the schedule
        public List<SchedulePeriod> periods { get; set; }
        public RoomConfig room { get; set; }


    }
}
