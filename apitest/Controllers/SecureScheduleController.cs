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
    [Route("api/Schedules")]
    [ApiController]
    public class SecureScheduleController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public SecureScheduleController(IConfiguration configuration) 
        {
            //throw new Exception("Sched Controller Exists");
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

        // get all of the schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> getSchedules() 
        {
            Console.WriteLine("Getting Schedules");
            printExampleJson();
            return await _context.schedules.Select(x => x).ToListAsync();
        }

        // get schedule by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(long id, string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                var test = _context.schedules.Include("periods").Include("lightConfigs");

                // TODO find a way to make this async
                var schedule = test.Where(x => x.ScheduleId == id).ToArray<Schedule>();

                if (schedule == null)
                {
                    return NotFound();
                }
                if (schedule.Length > 1) // this means that theres several schedules with the same id which 
                                         //SHOULD be impossible with the database
                {
                    Console.WriteLine("More than one result found");
                    return Problem("More than one result was found");
                }

                return schedule[0];
            }
        }

        // update an existing schedule
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(long id, Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return BadRequest();
            }

            var newschedule = await _context.schedules.FindAsync(id);
            if (newschedule == null)
            {
                return NotFound();
            }

            newschedule.name = schedule.name;
            newschedule.periods = schedule.periods;
            //newschedule.room = schedule.room;
            newschedule.delay = schedule.delay;
            //newschedule.DeviceId = schedule.DeviceId;
            newschedule.intensity = schedule.intensity;
            //newschedule.lightPort = schedule.lightPort;
            //newschedule.sensorPort = schedule.sensorPort;
            newschedule.lightConfigs = schedule.lightConfigs;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ScheduleExists(id))
            {
                {
                    return NotFound();
                }

            }

            return NoContent();
        }
        /*
        [Route("api/Schedules")]
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            
            var newDevice = new Device
            {
                Name = device.name,
                periods = schedule.periods,
                delay = schedule.delay,
                intensity = schedule.intensity,
                lightConfigs = schedule.lightConfigs
            };
            
            _context.devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("created device", device);
        }
        */

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostSchedule(Schedule schedule)
        {

            var newSchedule = new Schedule
            {
                name = schedule.name,
                periods = schedule.periods,
                delay = schedule.delay,
                intensity = schedule.intensity,
                lightConfigs = schedule.lightConfigs

        };

            _context.schedules.Add(newSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = newSchedule.ScheduleId }, newSchedule);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> DeleteSchedule(long id)
        {
            var schedule = await _context.schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            _context.schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(long id) =>
            _context.schedules.Any(e => e.ScheduleId == id);

        private void printExampleJson() 
        {
            Schedule newsched = new Schedule { name = "testsched", delay = 100, lightConfigs = { }, intensity = 255 };
            List<SchedulePeriod> newperiods = new List<SchedulePeriod>();
            newperiods.Add(new SchedulePeriod { startTime = new Time { hours = 8, minutes = 22 }, endTime = new Time { hours = 9, minutes = 35 } });
            newperiods.Add(new SchedulePeriod { startTime = new Time { hours = 13, minutes = 45 }, endTime = new Time { hours = 15, minutes = 21 } });
            List<LightConfig> lightConfigs = new List<LightConfig>();
            lightConfigs.Add(new LightConfig(1, 3, 4));
            lightConfigs.Add(new LightConfig(2, 3, 4));
            newsched.periods = newperiods;
            newsched.lightConfigs = lightConfigs;

            Device newDevice = new Device { Name = "RoomDevice", room = new RoomConfig{ roomHeight = 10, roomWidth = 12 }, schedules = { } };
            newDevice.schedules = new List<Schedule>();
            newDevice.schedules.Add(newsched);



            Console.WriteLine(JsonSerializer.Serialize(newDevice));
        }
    }
}
