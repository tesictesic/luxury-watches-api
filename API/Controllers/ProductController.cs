using Application.DTO;
using Application.DTO.Product;
using Application.DTO.Searches;
using Application.UseCases.Commands.ProductCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UseCaseHandler useCaseHandler;
        public ProductController(UseCaseHandler handler)
        {
            this.useCaseHandler= handler;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get([FromBody] ProductSearchDTO dto, [FromServices] IGetProductQuery query)
        {
            try
            {
                return Ok(this.useCaseHandler.HandleQuery(query, dto));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProductSinglePage query)
        {
            ProductSinglePageDTO dto = new ProductSinglePageDTO
            {
                Id = id
            };
            try
            {
                return Ok(this.useCaseHandler.HandleQuery(query, dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromForm] ProductDTO dto,ICreateProductCommand command)
        {
            try
            {
                useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteProductCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            try
            {
               useCaseHandler.HandleCommand(command,dto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
