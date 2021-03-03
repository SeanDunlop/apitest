using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apitest.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace apitest
{
    public class Program
    {

        public static void Main(string[] args)
        {
            /*
            var newSchedule = new Schedule
            {
                ScheduleId = 1,
                name = "test schedule",
                startTime = 100,
                endTime = 200,
                device = new Device 
                {
                    DeviceId = 1,
                    Name = "test device"
                }

            };

            Console.WriteLine(JsonSerializer.Serialize(newSchedule));
            */
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
