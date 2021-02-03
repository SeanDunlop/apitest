using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Controllers
{
    [ApiController]
    public class ApiTestController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public ApiTestController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public String Get()
        {
            var rng = new Random();
            return "This is a neat api";
        }
    }
}
