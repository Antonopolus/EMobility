using EMobility.Data;
using EMobility.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EMobility.WebApi.Services.ChargingSessionRepository;

namespace EMobility.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargingSessionsController : ControllerBase
    {
        readonly IChargingSessionRepository repository;

        public ChargingSessionsController(IChargingSessionRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/<ChargingSessionsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChargingSession>))]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/<ChargingSessionsController>/5
        [HttpGet("{id}", Name = nameof(GetSessionById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargingSession))]
        public IActionResult GetSessionById(int id)
        {
            var session = repository.GetById(id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        // POST api/<ChargingSessionsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ChargingSession))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ChargingSession value)
        {
            if (value.Id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var newSession = repository.Add(value);
            return CreatedAtAction(nameof(GetSessionById), new { id = newSession.Id }, newSession);
        }

        // PUT api/<ChargingSessionsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargingSession))]
        public IActionResult Put(int id, [FromBody] ChargingSession session)
        {
            var currentSession = repository.GetById(id);
            if (currentSession == null) return NotFound();

            var updatedSession = repository.Update(id, session);
            return CreatedAtAction(nameof(GetSessionById), new { id = updatedSession.Id }, updatedSession);
        }

        // delete api/<chargingsessionscontroller>/5
        [HttpDelete]
        [Route("{SessionToDeleteId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int SessionToDeleteId)
        {
            try
            {
                repository.Delete(SessionToDeleteId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
