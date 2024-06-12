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
        public IActionResult Get([FromQuery] GenderSearch searchDTO, [FromServices] IGetGenderQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query,searchDTO));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        // POST api/<GenderController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] GenderDTO genderDTO, [FromServices] ICreateGenderCommand command)
        {
            try
            {
                _handler.HandleCommand(command, genderDTO);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<GenderController>/5
        [HttpPut]
        public IActionResult Put ([FromBody] GenderDTO dto, [FromServices] IUpdateGenderCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(204);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<GenderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
