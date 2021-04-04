using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }

        public RoomConfig room { get; set; } // model of the room itself (size, height, etc.)

        public List<Schedule> schedules { get; set; }
    }
}
