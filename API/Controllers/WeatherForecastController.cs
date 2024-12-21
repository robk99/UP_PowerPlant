using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public AppDbContext DbContext { get; set; }
        public WeatherForecastController(AppDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "done";
        }
    }
}
