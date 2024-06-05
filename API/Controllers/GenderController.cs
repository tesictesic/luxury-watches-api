using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAcess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        // GET: api/<GenderController>
        private readonly IGetGender _query;
        public GenderController(IGetGender query)
        {
            this._query= query;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] GenderSearch searchDTO)
        {
            try
            {
                return Ok(_query.Execute(searchDTO));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        // GET api/<GenderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GenderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
