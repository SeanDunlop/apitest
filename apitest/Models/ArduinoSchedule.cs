using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class ArduinoSchedule
    {
        public ArduinoSchedule()
        {
            length = 0;
            sensors = new List<int>();
            lights = new List<int>();
            delay = new List<int>();
            duration = new List<int>();
            intensity = new List<int>();
        }
        public int length { get; set; }
        public List<int> sensors { get; set; }
        public List<int> lights { get; set; }
        public List<int> delay { get; set; }
        public List<int> duration { get; set; }
        public List<int> intensity { get; set; }
    }
}
