using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace apitest
{
    public class requestSender
    {
        /*
        static void Main(string[] args) 
        {
            

            postrequest request = new postrequest("Test Request");
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:44352/test1";

            // List all Names.    
            //HttpResponseMessage response = client.GetAsync("api/Values").Result;  // Blocking call!    
            //HttpResponseMessage response = client.GetAsync("api/Values").Result;  // Blocking call!    
            using var client = new HttpClient();

            var response =  client.PostAsync(url, data);

            string result = response.Result.ToString();
            Console.WriteLine(result);

        }
        */
    }
}
