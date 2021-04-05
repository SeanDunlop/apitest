using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
namespace apitest.Models
{
    public class ScheduleContext : DbContext 
    {
        public ScheduleContext(string connection) : base(connection)
        {
        }

        public ScheduleContext() { }
        public DbSet<Device> devices { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<User> creds { get; set; }
        public DbSet<Session> sessions { get; set; }
    }
}
