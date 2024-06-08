using Application.DTO;
using Application.UseCases.Commands.UserCommands;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        public UserController(UseCaseHandler useCaseHandler)
        {
            this._useCaseHandler = useCaseHandler;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromForm] RegisterDTO registerDTO, [FromServices] IUserRegisterCommand command)
        {
            try
            
            {
                _useCaseHandler.HandleCommand(command, registerDTO);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
