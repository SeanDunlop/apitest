using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; } // id for the table
        public string name { get; set; } // name of the schedule
        public List<SchedulePeriod> periods { get; set; } // set of periods of time for the lights to be on
        public RoomConfig room { get; set; } // model of the room itself (size, height, etc.)


    }
}
