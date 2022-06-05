using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess;
using Implementation.Commands;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public PublishersController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }

        // GET: api/<PublishersController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] PublisherSearch search,
            [FromServices] IGetPublishersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<PublishersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetPublisherQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<PublishersController>
        [HttpPost]
        public IActionResult Post([FromBody] PublisherDTO dto,
            [FromServices] ICreatePublisherCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PublisherDTO dto, [FromServices] IUPdatePublisherCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePubliserCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
