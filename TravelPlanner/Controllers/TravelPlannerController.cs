using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelPlannerLibrary;

namespace TravelPlanner.Controllers {
    [ApiController]
    [Route("api/travelPlan")]
    public class TravelPlannerController : ControllerBase {
        private readonly HttpClient _client;
        private readonly ILogger<TravelPlannerController> _logger;
        private List<Route> Routes = new List<Route>();

        public TravelPlannerController(ILogger<TravelPlannerController> logger, IHttpClientFactory factory) {
            _logger = logger;
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri ("https://cddataexchange.blob.core.windows.net");
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string from, [FromQuery] string to, [FromQuery] string start) {
            // Check for errors
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(start)) {
                _logger.LogWarning("Invalid parameters.");
                return BadRequest();
            }

            var response = await _client.GetAsync("/data-exchange/htl-homework/travelPlan.json");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Routes = JsonSerializer.Deserialize<List<Route>>(json);

            RouteFinder RF = new RouteFinder(Routes);
            var twl = RF.GetFastestRoute(from, to, start);


            return Ok(twl);
        }
    }
}
