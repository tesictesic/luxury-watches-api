using Application.DTO;
using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Commands.BrandsCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromBody] LookupSearch search, [FromServices] IGetBrandQuery query)
        {
           
            
                _handler.HandleQuery(query,search);
                return Ok(_handler.HandleQuery(query, search));
            
           
        }
        // POST api/<BrandController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] BrandDTO dto, [FromServices] ICreateBrandCommand command)
        {

            _handler.HandleCommand(command, dto);
            return Created();
        }

        // PUT api/<BrandController>/5
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] BrandDTO dto, [FromServices] IUpdateBrandCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(203);
        }

        // DELETE api/<BrandController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
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
