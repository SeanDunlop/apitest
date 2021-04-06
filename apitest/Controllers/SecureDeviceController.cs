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
    [Route("api/SecureDevices")]
    [ApiController]
    public class SecureDeviceController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public SecureDeviceController(IConfiguration configuration)
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
        public async Task<ActionResult<IEnumerable<Device>>> getDevices(string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                Console.WriteLine("Getting Devices");
                //printExampleJson();
                return await _context.devices.Where(x => x.Owner == userId).ToListAsync();
            }

        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostDevice([FromBody] Device device, [FromQuery] string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                var newDevice = new Device
                {
                    Name = device.Name,
                    room = device.room,
                    schedules = device.schedules,
                    xpos = device.xpos,
                    ypos = device.ypos
                };

                _context.devices.Add(newDevice);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDevice), new { id = newDevice.DeviceId }, newDevice);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(long id, string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                var test = _context.devices.Include("schedules.periods").Include("schedules.lightConfigs.sensorPorts");
                var device = test.Where(x => x.DeviceId == id && x.Owner == userId).ToArray<Device>();

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


            // TODO find a way to make this async

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDevice([FromQuery] long id,[FromBody] Device device, [FromQuery] string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                if (id != device.DeviceId || device.Owner != userId)
                {
                    return BadRequest();
                }

                var test = _context.devices.Include("schedules.periods").Include("schedules.lightConfigs.sensorPorts");

                // TODO find a way to make this async
                var result = test.Where(x => x.DeviceId == id).ToArray<Device>();
                var newDevice = result[0];
                if (newDevice == null)
                {
                    return NotFound();
                }

                newDevice.Name = device.Name;
                newDevice.room = device.room;
                newDevice.schedules = device.schedules;
                newDevice.xpos = device.xpos;
                newDevice.ypos = device.ypos;
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
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> DeleteDevice(long id, string token)
        {
            int userId = new SessionManager().isValid(_context, token);
            if (userId == 0)
            {
                return Forbid();
            }
            else
            {
                var device = await _context.devices.FindAsync(id);

                if (device == null)
                {
                    return NotFound();
                }
                if(device.Owner != userId)
                {
                    return BadRequest();
                }

                _context.devices.Remove(device);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        private bool DeviceExists(long id) =>
    _context.devices.Any(e => e.DeviceId == id);
    }

}
