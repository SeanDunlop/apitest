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
    [Route("api/Login")]
    [ApiController]
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
        public string login(string username, string hashpass)
        {
            Console.WriteLine("Logging In");
            //printExampleJson();
            var user = _context.creds.Where(x => x.Username == username && x.Password == hashpass);
            if(!user.Any())
            {
                return null;
            }
            else
            {
                Session s = new Session();
                s.UserId = user.First().UserId;
                s.SessionGuid = Guid.NewGuid().ToString();
                s.Timeout = DateTime.UtcNow.AddMinutes(100);
                _context.sessions.Add(s);
                _context.SaveChanges();
                return s.SessionGuid;
            }
        }
        /*
        [HttpGet]
        public async Task<ActionResult<string>> logout(string token)
        {
            var session = _context.sessions.Where(x => x.SessionGuid == token);
            session.First().Timeout = DateTime.UtcNow.AddHours(-1);
            await _context.SaveChangesAsync();
            return Ok();
        }
        */
    }
}
