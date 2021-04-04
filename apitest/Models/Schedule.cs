using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Schedule
    {
        //schedule info
        public int ScheduleId { get; set; }
        //public int DeviceId { get; set; }
        public string name { get; set; } 

        // light settings
        public int delay { get; set; }
        public int intensity { get; set; }
        public List<LightConfig> lightConfigs { get; set; }

        //periods
        public List<SchedulePeriod> periods { get; set; } // set of periods of time for the lights to be on
        


    }
}
