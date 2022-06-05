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
    public class BookAuthorController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public BookAuthorController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }

        // GET: api/<BookAuthorController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] BookAuthorSearch search,
            [FromServices] IGetBookAuthorQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<BookAuthorController>
        [HttpPost]
        public IActionResult Post([FromBody] BookAuthorDTO dto,
            [FromServices] ICreateBookAuthorCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<BookAuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookAuthorDTO dto, [FromServices] IUpdateBookAuthorCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<BookAuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookAuthorCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
