using API.Core;
using Application.DTO.User;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                Guid tokenId = _jwtManager.GetTokenIdFromJwt(token);
                _jwtManager.RemoveToken(tokenId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
