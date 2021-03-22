using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apitest.Models;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
namespace apitest.Controllers
{
    [Route("api/Schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public ScheduleController(IConfiguration configuration) 
        {
            _configuration = configuration;

            String conn;
            try { conn = _configuration.GetConnectionString("Database"); }
            catch (Exception e) 
            {
                conn = "the connection string failed";
                throw new Exception("string was " + conn);
            }
            //
            _context = new ScheduleContext(conn);
            
        }

        // get all of the schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> getSchedules() 
        {

            Console.WriteLine("Getting Schedules");
            Schedule newsched = new Schedule {name = "testsched", room = new RoomConfig {roomWidth=2, roomHeight=4 } };
            List<SchedulePeriod> newperiods = new List<SchedulePeriod>();
            newperiods.Add(new SchedulePeriod { startTime = DateTime.Now, endTime = DateTime.MaxValue, intensity = 2 });
            newperiods.Add(new SchedulePeriod { startTime = DateTime.Now, endTime = DateTime.MaxValue, intensity = 3 });
            newsched.periods = newperiods;
            Console.WriteLine(JsonSerializer.Serialize(newsched));
            return await _context.schedules.Select(x => x).ToListAsync();
        }

        // get schedule by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(long id)
        {
            //var x = _context.schedules.Include<SchedulePeriod>(sp => sp.periods)
            //                          .FindAsync(id);
            //namespace 
            var test = _context.schedules.Include("periods");

            var schedule = test.Where(x => x.ScheduleId == id).ToArray<Schedule>();

            if (schedule == null)
            {
                return NotFound();
            }
            if (schedule.Length > 1) 
            {
                Console.WriteLine("More than one result found");
                return Problem("More than one result was found");
            }

            return schedule[0];
        }

        // update an existing schedule
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(long id, Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return BadRequest();
            }

            //_context.Entry(todoItemDTO).State = EntityState.Modified;
            var newschedule = await _context.schedules.FindAsync(id);
            if (newschedule == null)
            {
                return NotFound();
            }

            newschedule.name = schedule.name;
            newschedule.periods = schedule.periods;
            newschedule.room = schedule.room;

            //newschedule.startTime = schedule.startTime;
            //newschedule.endTime = schedule.endTime;
            //newschedule.device = schedule.device;

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

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostSchedule(Schedule schedule)
        {

            var newSchedule = new Schedule
            {
                name = schedule.name,
                periods = schedule.periods,
                room= schedule.room,
                //device = schedule.device

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
    }
}
