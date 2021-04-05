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

namespace apitest.Controllers
{
    [Route("api/Devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public DeviceController(IConfiguration configuration)
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> getDevices()
        {
            Console.WriteLine("Getting Devices");
            //printExampleJson();
            return await _context.devices.Select(x => x).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostDevice(Device device)
        {

            var newDevice = new Device
            {
                Name = device.Name,
                room = device.room,
                schedules = device.schedules,
            };

            _context.devices.Add(newDevice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = newDevice.DeviceId }, newDevice);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(long id)
        {

            var test = _context.devices.Include("schedules.periods").Include("schedules.lightConfigs.sensorPorts");

            // TODO find a way to make this async
            var device = test.Where(x => x.DeviceId == id).ToArray<Device>();

            if (device == null)
            {
                return NotFound();
            }
            if (device.Length > 1) // this means that theres several schedules with the same id which 
                                     //SHOULD be impossible with the database
            {
                Console.WriteLine("More than one result found");
                return Problem("More than one result was found");
            }
            
            return device[0];
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(long id, Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            var newDevice = await _context.devices.FindAsync(id);
            if (newDevice == null)
            {
                return NotFound();
            }

            newDevice.Name = device.Name;
            newDevice.room = device.room;
            newDevice.schedules = device.schedules;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DeviceExists(id))
            {
                {
                    return NotFound();
                }

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> DeleteDevice(long id)
        {
            var device = await _context.devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            _context.devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(long id) =>
    _context.devices.Any(e => e.DeviceId == id);
    }

}
