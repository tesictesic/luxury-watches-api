using Application.DTO.Cart;
using Application.DTO.Searches;
using Application.UseCases.Commands.CartCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public CartController(UseCaseHandler handler)
        {
            this._useCaseHandler = handler;
        }
        [HttpGet]
        public IActionResult Get([FromBody]CartSearchDTO dto, [FromServices] IGetCartQuery query)
        {
            return Ok(_useCaseHandler.HandleQuery(query, dto));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CartDTO dTO,ICreateCartCommand command)
        {
            _useCaseHandler.HandleCommand(command, dTO);
            return StatusCode(201);
        }

        

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
