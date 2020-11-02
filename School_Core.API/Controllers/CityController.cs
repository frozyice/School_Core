using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School_Core.API.Model;

namespace School_Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Database.store.citys);

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = Database.store.citys.FirstOrDefault(x => x.Id == id);
            if (city is null)
                return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public IActionResult Post(City city)
        {
            
            return Ok();
        }
    }
}