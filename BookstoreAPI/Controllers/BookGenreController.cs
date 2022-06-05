using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess;
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
    public class BookGenreController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public BookGenreController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }
        // GET: api/<BookGenreController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] BookGenreSearch search,
            [FromServices] IGetBookGenreQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<BookGenreController>
        [HttpPost]
        public IActionResult Post([FromBody] BookGenreDTO dto,
            [FromServices] ICreateBookGenreCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<BookGenreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookGenreDTO dto, [FromServices] IUpdateBookGenreCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<BookGenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookGenreCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
