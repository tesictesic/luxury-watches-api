using Application.DTO;
using Application.DTO.Product;
using Application.DTO.Searches;
using Application.UseCases.Commands.ProductCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get([FromQuery] ProductSearchDTO dto, [FromServices] IGetProductQuery query)
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
            return Ok(this.useCaseHandler.HandleQuery(query, dto));
        }

        // POST api/<ProductController>
        
        [HttpPost]
        public IActionResult Post([FromForm] ProductDTO dto,[FromServices]ICreateProductCommand command)
        {
            
            
                useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);
            
           
        }

        // PUT api/<ProductController>/5
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromForm] ProductDTO dto, [FromServices]IUpdateProductCommande command)
        {
            useCaseHandler.HandleCommand(command,dto);
            return StatusCode(203);
        }

        // DELETE api/<ProductController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteProductCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            useCaseHandler.HandleCommand(command, dto);
            return NoContent();

        }
    }
}
