using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apitest.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure; 


namespace apitest.Controllers
{
    [Route("api/Schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleContext _context;

        public ScheduleController() 
        {
            _context = new ScheduleContext(CloudConfigurationManager.GetSetting("Database"));
            //_context = new ScheduleContext("Server=tcp:luxo-server.database.windows.net,1433;Initial Catalog=luxo-db;Persist Security Info=False;User ID=luxo;Password=P1x@r123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        // get all of the schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> getSchedules() 
        {
            return await _context.schedules.Select(x => x).ToListAsync();
        }

        // get schedule by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(long id)
        {
            var schedule = await _context.schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
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
            newschedule.startTime = schedule.startTime;
            newschedule.endTime = schedule.endTime;
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
                startTime = schedule.startTime,
                endTime = schedule.endTime,
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
