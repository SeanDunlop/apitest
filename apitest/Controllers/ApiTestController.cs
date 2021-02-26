using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using apitest.Models;
namespace apitest.Controllers
{
    [ApiController]
    public class ApiTestController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        //private readonly TodoContext _context;

        public ApiTestController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/test")]
        public String Get()
        {
            var rng = new Random();
            Console.WriteLine("Api was called");
            return "This is a neat api";
        }

        [HttpGet]
        [Route("/test1")]
        public String testCall(string request) 
        {
            return request + "Was Sent";
        }
        /*
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }


        [HttpGet("{id}")]
        [Route("/api/ApiTest/route")]
        public async IActionResult testRoute(string id)
        {
            return "id was " + id;
        }
        */

        [HttpPost]
        [Route("/posttest")]
        public HttpResponseMessage Post([FromBody] postrequest request) 
        {
            Console.WriteLine("Recieved " + request.input);
            if (request.input == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            else 
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            }
            
        }

    }
}
