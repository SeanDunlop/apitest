using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class ReducedSchedule
    {
        public int ScheduleId { get; set; } // id for the table
        public int DeviceId { get; set; }
        public int delay { get; set; }
        public int intensity { get; set; }
        public List<LightTuple> lights;
        public SchedulePeriod currentPeriod { get; set; }


        public ReducedSchedule() { }

        public ReducedSchedule(Schedule sched)
        {
            this.ScheduleId = sched.ScheduleId;
            this.delay = sched.delay;
            this.intensity = sched.intensity;

            foreach (LightConfig config in sched.lightConfigs) 
            {
                foreach (SensorPort sensor in config.sensorPorts) 
                {
                    lights.Add(new LightTuple { lightPort = config.lightPort, sensorPort = sensor.port });
                }
            }


            int hours = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;

            SchedulePeriod nextPeriod;
            foreach (SchedulePeriod period in sched.periods) // only gives the current period right now
            {
                //has the period started
                if (period.startTime.hours < hours ||
                    (period.startTime.hours == hours && period.startTime.minutes <= min))
                {
                    if (period.endTime.hours > hours ||
                        (period.endTime.hours == hours && period.endTime.minutes > min)) 
                    {
                        nextPeriod = period;
                    }
                }
            }

        }
    }

    public class LightTuple
    {
        public int lightPort { get; set; }
        public int sensorPort { get; set; }
    }
}
