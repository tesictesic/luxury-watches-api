using Application.DTO;
using Application.DTO.Lookup;
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
        public IActionResult Put([FromBody] ColorDTO dto, [FromServices] IUpdateColorCommand command)
        {
            try
            {
                _useCaseHandler.HandleCommand(command, dto);
                return StatusCode(203);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
    }
}

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,IDeleteColorCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            try
            {
                _useCaseHandler.HandleCommand(command, dto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
