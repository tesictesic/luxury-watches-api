using Application.DTO;
using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Commands.GenderCommands;
using Application.UseCases.Queries;
using DataAcess;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private UseCaseHandler _handler;
        public GenderController(UseCaseHandler handler)
        {
            this._handler = handler;
        }
        // GET: api/<GenderController>

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] LookupSearch searchDTO, [FromServices] IGetGenderQuery query)
        {
            return Ok(_handler.HandleQuery(query, searchDTO));
        }

        // POST api/<GenderController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] GenderDTO genderDTO, [FromServices] ICreateGenderCommand command)
        {
            _handler.HandleCommand(command, genderDTO);
            return StatusCode(201);
        }

        // PUT api/<GenderController>/5
        [Authorize]
        [HttpPut]
        public IActionResult Put ([FromBody] GenderDTO dto, [FromServices] IUpdateGenderCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<GenderController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteGenderCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
