using Application.DTO;
using Application.DTO.Searches;
using Application.DTO.User;
using Application.UseCases.Commands.UserCommands;
using Application.UseCases.Queries;
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
        public IActionResult Get([FromBody] UserSerachDTO dto, [FromServices] IGetUserQuery query)
        {
            try
            {
                return Ok(_useCaseHandler.HandleQuery(query, dto));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public IActionResult Put([FromForm] RegisterDTO dto, [FromServices] IUserUpdateCommand command)
        {
            try
            {
                this._useCaseHandler.HandleCommand(command, dto);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices]IUserDeleteCommand command)
        {
            try
            {
                DeleteDTO dto = new DeleteDTO
                {
                    Id = id
                };
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
