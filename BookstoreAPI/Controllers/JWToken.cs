using BookstoreAPI.Core;
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
    public class JWToken : ControllerBase
    {
        private readonly JWTManager _manager;

        public JWToken(JWTManager manager)
        {
            _manager = manager;
        }

        // POST api/<JWToken>
        [HttpPost]
        public IActionResult Post([FromBody] Login request)
        {
            var token = _manager.MakeToken(request.Username, request.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}
