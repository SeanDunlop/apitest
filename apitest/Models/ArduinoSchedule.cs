using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class ArduinoSchedule
    {
        public int length { get; set; }
        public List<int> sensors { get; set; }
        public List<int> lights { get; set; }
        public List<int> delay { get; set; }
        public List<int> duration { get; set; }
        public List<int> intensity { get; set; }
    }
}
