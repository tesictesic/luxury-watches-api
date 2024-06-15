using Application.DTO.Searches;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;
        public AuditLogController(UseCaseHandler caseHandler)
        {
            this._useCaseHandler = caseHandler;
        }
        // GET: api/<AuditLogController>
        [HttpGet]
        public IActionResult Get([FromBody] AuditLogSearchDTO searchDTO, [FromServices] IGetAuditLogQuery query)
        {
           
                
                return Ok(_useCaseHandler.HandleQuery(query, searchDTO));
            
            
        }
    }
}
