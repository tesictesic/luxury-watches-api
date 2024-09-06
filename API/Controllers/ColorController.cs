using Application.DTO;
using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Commands.ColorCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get([FromQuery] LookupSearch search, [FromServices] IGetColorQuery query)
        {
            return Ok(_useCaseHandler.HandleQuery(query, search));
        }
        // POST api/<ColorController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ColorDTO dTO, [FromServices] ICreateColorCommand command)
        {
            _useCaseHandler.HandleCommand(command, dTO);
            return StatusCode(201);
        }

        // PUT api/<ColorController>/5
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] ColorDTO dto, [FromServices] IUpdateColorCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(203);
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteColorCommand command)
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
