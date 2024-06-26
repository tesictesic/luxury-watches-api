﻿using Application.DTO;
using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Commands.SpecificationCommands;
using Application.UseCases.Queries;
using Implementation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromBody] LookupSearch search, [FromServices] IGetSpecificationQuery query)
        {
            return Ok(this.useCaseHandler.HandleQuery(query, search));
        }

        // GET api/<SpecificationController>/5
        

        // POST api/<SpecificationController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] SpecificationDTO specificationDTO, [FromServices] ICreateSpecificationCommand command)
        {
            this.useCaseHandler.HandleCommand(command, specificationDTO);
            return StatusCode(201);
        }

        // PUT api/<SpecificationController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put([FromBody] SpecificationDTO dto, [FromServices] IUpdateSpecificationCommand command)
        {
            useCaseHandler.HandleCommand(command, dto);
            return StatusCode(203);
        }

        // DELETE api/<SpecificationController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteSpecificationCommand command)
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
