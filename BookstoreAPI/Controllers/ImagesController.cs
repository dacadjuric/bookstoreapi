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
    public class ImagesController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IApplicatioActor _actor;
        private readonly UseCaseExecutor _executor;

        public ImagesController(BookstoreContext context, IApplicatioActor actor, UseCaseExecutor executor)
        {
            _context = context;
            _actor = actor;
            _executor = executor;
        }

        [Authorize]
        // GET: api/<ImagesController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] ImageSearch search,
            [FromServices] IGetImagesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<ImagesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetImageQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<ImagesController>
        [HttpPost]
        public IActionResult Post([FromBody] ImageDTO dto,
            [FromServices] ICreateImageCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ImagesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ImageDTO dto, [FromServices] IUpdateImageCommand command)
        {
            dto.Id = id;

            _executor.ExecuteCommand(command, dto);

            return NoContent();
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteImageCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
