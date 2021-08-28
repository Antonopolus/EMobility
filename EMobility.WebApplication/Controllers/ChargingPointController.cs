using EMobility.WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EMobility.WebApplication.Services.ChargingPointsRepository;

namespace EMobility.WebApplication.Controllers
{
    [ApiController]
    [Route("api/ChargingPoints")]
    public class ChargingPointController : ControllerBase
    {
        private readonly IChargingPointsRepository repository;

        public ChargingPointController(ILogger<ChargingPointController> logger, IChargingPointsRepository repo)
        {
            repository = repo;
            logger.LogInformation("setup ChargingPointController");

            foreach (var cp in repository.GetAll())
            {
                logger.LogInformation(cp.Name);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChargingPointMod>))]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargingPointMod))]
        public IActionResult GetById(int id)
        {
            var cp = repository.GetById(id);
            if (cp == null) return NotFound();
            return Ok(cp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ChargingPointMod))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] ChargingPointMod cp)
        {
            if(cp.Id == 0)
            {
                return BadRequest("Invalid ID");
            }

            var newCp = repository.Add(cp);
            return CreatedAtAction(nameof(GetById), new { id = newCp.Id}, newCp);
        }

        [HttpDelete]
        [Route("{ChargingPointToDeleteId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int chargingPointToDeleteId)
        {
            try
            {
                repository.Delete(chargingPointToDeleteId);
            }
            catch (ArgumentException )
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
