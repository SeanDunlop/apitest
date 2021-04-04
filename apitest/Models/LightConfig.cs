using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    public class LightConfig
    {
        public int LightConfigId { get; set; }
        public int lightPort { get; set; }
        public List<SensorPort> sensorPorts { get; set; }

        public LightConfig(int light, params int[] sensors) 
        {
            sensorPorts = new List<SensorPort>();
            lightPort = light;
            foreach (int port in sensors) 
            {
                sensorPorts.Add(new SensorPort { port = port});
            }
        }

        public LightConfig() { }
    }
}
