using Application.DTO.Contacts;
using Application.UseCases.Commands.ContactCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public ContactController(UseCaseHandler handler)
        {
            this._useCaseHandler = handler;
        }
        // GET: api/<ContactController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SearchContactDTO dto,IGetContactQuery query)
        {
            return Ok(this._useCaseHandler.HandleQuery(query, dto));  
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactController>
        [HttpPost]
        public IActionResult Post([FromBody] InsertContactDTO dto,ICreateContact command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return Created();
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
