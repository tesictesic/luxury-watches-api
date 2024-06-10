using API.Core;
using Application.DTO.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        // GET: api/<JwtController>
        private JWTManager _jwtManager;
        public JwtController(JWTManager manager)
        {
                this._jwtManager = manager;
        }

        // POST api/<JwtController>
        [HttpPost]
        public IActionResult Post([FromBody] UserLoginDTO dto)
        {
            return Ok(new
            {
                token=_jwtManager.MakeToken(dto.Email, dto.Password),
            });
        }


    }
}
