using Application.DTO;
using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Commands.BrandsCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private UseCaseHandler _handler;
        public BrandController(UseCaseHandler handler)
        {
            this._handler = handler;
        }

        // GET: api/<BrandController>
        [HttpGet]
        public IActionResult Get([FromBody] LookupSearch search, [FromServices] IGetBrandQuery query)
        {
            try
            {
                _handler.HandleQuery(query,search);
                return Ok(_handler.HandleQuery(query, search));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        // POST api/<BrandController>
        [HttpPost]
        public IActionResult Post([FromBody] BrandDTO dto, [FromServices] ICreateBrandCommand command)
        {
            try
            {
                _handler.HandleCommand(command,dto);
                return Created();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BrandController>/5
        [HttpPut]
        public IActionResult Put([FromBody] BrandDTO dto, [FromServices] IUpdateBrandCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(203);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            try
            {
                _handler.HandleCommand(command, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
