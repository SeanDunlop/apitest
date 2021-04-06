using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data.Entity;
using apitest.Common;
namespace apitest.Controllers
{
    [Route("api/ArduinoSchedule")]
    [ApiController]
    public class ArduinoScheduleController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public ArduinoScheduleController(IConfiguration configuration)
        {

            _configuration = configuration;

            String conn;
            try { conn = _configuration.GetConnectionString("Database"); }
            catch (Exception e)
            {
                conn = "the connection string failed";
                throw new Exception("string was " + conn);
            }
            _context = new ScheduleContext(conn);
        }
        //*
        [HttpGet]
        public async Task<ActionResult<ArduinoSchedule>> getSchedule(string arduinoId)
        {
            var test = _context.devices.Include("schedules.periods").Include("schedules.lightConfigs.sensorPorts");
            var device = test.Where(x => x.DeviceGuid == arduinoId).ToArray<Device>();

            if(device == null)
            {
                return null;
            }
            else
            {
                Schedule s = device.First().schedules[0];
                ArduinoSchedule a = new ArduinoSchedule();
                int duration = 0;
                int delay = s.delay;
                int intensity = s.intensity;

                foreach(SchedulePeriod p in s.periods)
                {
                    int nowHour = DateTime.UtcNow.Hour;
                    int nowMin = DateTime.UtcNow.Minute;
                    if(p.startTime.hours == nowHour && p.startTime.minutes < nowMin)
                    {
                        if(p.endTime.hours > nowHour)
                        {
                            //in this period
                            duration = p.duration;
                            break;
                        }else if(p.endTime.hours == nowHour && p.endTime.minutes > nowMin)
                        {
                            //in this period
                            duration = p.duration;
                            break;
                        }
                    }else if(p.startTime.hours < nowHour)
                    {
                        if (p.endTime.hours > nowHour)
                        {
                            //in this period
                            duration = p.duration;
                            break;
                        }
                        else if (p.endTime.hours == nowHour && p.endTime.minutes > nowMin)
                        {
                            //in this period
                            duration = p.duration;
                            break;
                        }
                    }
                }
                List<(int, int)> pairs = new List<(int,int)>();
                foreach(LightConfig l in s.lightConfigs) {
                    pairs.Add((l.lightPort, l.sensorPorts[0].port));
                    /*
                    foreach(SensorPort sp in l.sensorPorts)
                    {
                        pairs.Add((l.lightPort, sp.port));
                    }
                    //*/
                }
                foreach((int,int) i in pairs)
                {
                    a.delay.Add(delay);
                    a.duration.Add(duration);
                    a.intensity.Add(intensity);
                    a.lights.Add(i.Item1);
                    a.sensors.Add(i.Item2);
                }
                a.length = pairs.Count;
                return a;
            }
        }
        //*/
    }
}
