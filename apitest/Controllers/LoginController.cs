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
    public class LoginController : ControllerBase
    {
        private readonly ScheduleContext _context;
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
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
        public async Task<ActionResult<string>> login(string username, string hashpass)
        {
            Console.WriteLine("Logging In");
            //printExampleJson();
            var user = _context.creds.Where(x => x.Username == username && x.Password == hashpass);
            if(user == null)
            {
                return Forbid();
            }
            else
            {
                Session s = new Session();
                s.userId = user.First().UserId;
                s.SessionGuid = Guid.NewGuid().ToString();
                TimeSpan delay = new TimeSpan(0, 30, 0);
                s.Timeout = DateTime.UtcNow + delay;
                _context.sessions.Add(s);
                await _context.SaveChangesAsync();
                return s.SessionGuid;
            }
        }
    }
}
