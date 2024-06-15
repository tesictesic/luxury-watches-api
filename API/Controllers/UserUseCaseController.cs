using Application.DTO;
using Application.UseCases.Commands.UseUserCaseCommands;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUseCaseController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public UserUseCaseController(UseCaseHandler handler)
        {
            this._useCaseHandler = handler;
        }
        // GET: api/<UserUseCaseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserUseCaseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserUseCaseController>
        [HttpPost]
        public IActionResult Post([FromBody] UserUseCaseDTO dto, ICreateUserUseCaseCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<UserUseCaseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserUseCaseController>/5
        [HttpDelete]
        public IActionResult Delete([FromBody] UserUseCaseDTO dto,[FromServices]IDeleteUserUseCaseCommand command)

        {
            _useCaseHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
