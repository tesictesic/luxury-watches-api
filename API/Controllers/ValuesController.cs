using DataAcess;
using Domain.LookupTables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ASPContext _context;
        public ValuesController(ASPContext context)
        {
            this._context= context;
        }
        [HttpGet]
        public IActionResult PutData()
        {
            try
            {
                Gender gender = new Gender
                {
                    Name = "Gender 1"
                };
                this._context.Genders.Add(gender);
                Gender gender2 = new Gender
                {
                    Name = "Gender 2"
                };
                this._context.Genders.Add(gender2);
                Gender gender3 = new Gender
                {
                    Name = "Gender 3"
                };
                this._context.Genders.Add(gender3);
                Gender gender4 = new Gender
                {
                    Name = "Gender 4"
                };
                this._context.Genders.Add(gender4);
                Gender gender5 = new Gender
                {
                    Name = "Gender 5"
                };
                this._context.Genders.Add(gender5);
                Gender gender6 = new Gender
                {
                    Name = "Gender 6"
                };
                this._context.Genders.Add(gender6);
                Gender gender7 = new Gender
                {
                    Name = "Gender 7"

                };
                this._context.Genders.Add(gender7);
                this._context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
