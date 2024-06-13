﻿using Application.DTO;
using Application.DTO.Lookup;
using Application.UseCases.Commands.SpecificationCommands;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private readonly UseCaseHandler useCaseHandler;
        public SpecificationController(UseCaseHandler useCaseHandler)
        {
            this.useCaseHandler = useCaseHandler;
        }
        // GET: api/<SpecificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SpecificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpecificationController>
        [HttpPost]
        public IActionResult Post([FromBody] SpecificationDTO specificationDTO, [FromServices] ICreateSpecificationCommand command)
        {
            try
            {
                this.useCaseHandler.HandleCommand(command, specificationDTO);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SpecificationController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] SpecificationDTO dto, [FromServices] IUpdateSpecificationCommand command)
        {
            try
            {
                useCaseHandler.HandleCommand(command, dto);
                return StatusCode(203);
            }
           
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
    }
}

        // DELETE api/<SpecificationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, IDeleteSpecificationCommand command)
        {
            DeleteDTO dto = new DeleteDTO
            {
                Id = id
            };
            try
            {
                useCaseHandler.HandleCommand(command, dto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}