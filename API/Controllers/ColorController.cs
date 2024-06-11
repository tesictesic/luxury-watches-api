using Application.DTO;
using Application.UseCases.Commands.ColorCommands;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public ColorController(UseCaseHandler handler)
        {
            this._useCaseHandler = handler;
        }
        // GET: api/<ColorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ColorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ColorController>
        [HttpPost]
        public IActionResult Post([FromBody] ColorDTO dTO, [FromServices] ICreateColorCommand command)
        {
            try
            {
                _useCaseHandler.HandleCommand(command, dTO);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ColorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
