using AutoMapper;
using EMobility.Data;
using EMobility.WebApi.Dtos;
using EMobility.WebApi.Services.Async;
using EMobility.WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargingPointController : ControllerBase
    {
        private readonly IChargingPointsService service;
        private readonly IMessageBusClient MessageBus;
        public readonly IMapper Mapper;

        public ChargingPointController(ILogger<ChargingPointController> logger, 
                                        IChargingPointsService service, 
                                        IMessageBusClient messageBus,
                                        IMapper mapper)
        {
            this.service = service;
            MessageBus = messageBus;
            Mapper = mapper;
            logger.LogInformation("setup ChargingPointController");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChargingPointReadDto>))]
        public IActionResult GetAll()
        {
            var mapped = Mapper.Map<List<ChargingPointReadDto>>(service.GetAll());
            return Ok(mapped);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargingPointReadDto))]
        public IActionResult GetById(int id)
        {
            var cp = service.GetById(id);
            if (cp == null) return NotFound();
            return Ok(cp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ChargingPointCreateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] ChargingPointCreateDto cpCreateDto)
        {
            if (cpCreateDto.ChargingPointId.Length == 0)
            {
                return BadRequest("Invalid ChargingPointId");
            }
            var cp = Mapper.Map<ChargingPoint>(cpCreateDto);
            var newCp = service.Add(cp);
            var cpReadDto = Mapper.Map<ChargingPointReadDto>(newCp);
            try {
                MessageBus.PublishNewChargingPoint(newCp);
            }
            catch(Exception) { }

            return CreatedAtAction(nameof(GetById), new { id = newCp.Id }, cpReadDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
