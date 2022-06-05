using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using Bogus;
using DataAccess;
using Domain;
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
    public class BooksController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public BooksController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }

        // GET: api/<BooksController>
        [HttpGet]
        [Authorize]
        public IActionResult Get(
            [FromQuery] BookSearch search,
            [FromServices] IGetBooksQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
            //return Ok("Uslo je.");
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetBookQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] BookDTO dto,
            [FromServices] ICreateBookCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookDTO dto, [FromServices] IUpdateBookCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
