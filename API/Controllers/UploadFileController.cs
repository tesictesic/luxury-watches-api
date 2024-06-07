using API.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private static IEnumerable<string> allowedExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".png"
        };

        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine("wwwroot", "temp", fileName);

            return Ok(new { exists = Path.Exists(path) });
        }



        // POST api/<UploadFileController>
        [HttpPost]
        public IActionResult Post([FromForm] FileUploadDTO dTO)
        {
           
           var extension=Path.GetExtension(dTO.Image.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                return new UnsupportedMediaTypeResult();
            }
            var fileName = Guid.NewGuid().ToString() + extension;

            var savePath = Path.Combine("wwwroot", "temp", fileName);
            using var fs = new FileStream(savePath, FileMode.Create);
            dTO.Image.CopyTo(fs);

            return StatusCode(201, new { file = fileName });

        }

        // PUT api/<UploadFileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploadFileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
