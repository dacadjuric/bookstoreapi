using Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedingController : ControllerBase
    {
        // POST api/<SeedingController>
        [HttpPost]
        public IActionResult Post([FromServices] IFakeData faker)
        {
            faker.AddAuthors();
            faker.AddBooks();
            faker.AddGenre();
            faker.AddImage();
            faker.AddPublishers();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
