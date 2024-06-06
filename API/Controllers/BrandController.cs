using Application.DTO;
using Application.UseCases.Commands.BrandsCommands;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
