using Application.DTO;
using Application.DTO.Searches;
using Application.DTO.User;
using Application.UseCases.Commands.UserCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] UserSerachDTO dto, [FromServices] IGetUserQuery query)
        {
            return Ok(_useCaseHandler.HandleQuery(query, dto));
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneUser query)
        {
            
            return Ok(this._useCaseHandler.HandleQuery(query, id));
        }




        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromForm] RegisterDTO registerDTO, [FromServices] IUserRegisterCommand command)
        {
            _useCaseHandler.HandleCommand(command, registerDTO);
            return StatusCode(201);
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromForm] UpdateUser dto, [FromServices] IUserUpdateCommand command)
        {
            this._useCaseHandler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices]IUserDeleteCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            _useCaseHandler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
