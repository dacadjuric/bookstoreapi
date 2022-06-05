using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess;
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
    public class GenresController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public GenresController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }

        // GET: api/<GenresController>
        [Authorize]
        [HttpGet]
        public IActionResult Get(
            [FromQuery] GenreSearch search,
            [FromServices] IGetGenresQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetGenreQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<GenresController>
        [HttpPost]
        public IActionResult Post([FromBody] GenreDTO dto,
            [FromServices] ICreateGanreCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GenreDTO dto, [FromServices] IUpdateGenreCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteGenreCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
