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
    [Route("api/Arduino")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public ArduinoController(IConfiguration configuration)
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
        public string register(string username, string password)
        {
            var user = _context.creds.Where(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }
            else {
                var guid = Guid.NewGuid().ToString();
                var newDevice = new Device
                {
                    Owner = user.First().UserId,
                    Name = "New Device",
                    DeviceGuid = guid,
                    room = null,
                    schedules = null,
                };

                _context.devices.Add(newDevice);
                _context.SaveChangesAsync();

                return guid;
            }
        }

    }
}
