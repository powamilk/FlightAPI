using FlightAPI.PL.Services;
using FlightAPI.PL.ViewModel.Flight;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("GetAllFlights")]
        public IActionResult GetAllFlights()
        {
            var result = _flightService.GetList(out string errorMessage);
            if (result != null)
                return Ok(result);

            return NotFound(errorMessage);
        }

        [HttpGet("GetFlightById/{id}")]
        public IActionResult GetFlightById(int id)
        {
            var result = _flightService.GetById(id, out string errorMessage);
            if (result != null)
                return Ok(result);

            return NotFound(errorMessage);
        }

        [HttpPost("CreateFlight")]
        public IActionResult CreateFlight([FromBody] CreateFlightVM request)
        {
            var result = _flightService.Create(request, out string errorMessage);
            if (result)
            {
                var createdFlight = _flightService.GetById(_flightService.GetList(out _).Max(f => f.Id), out _);
                return CreatedAtAction(nameof(GetFlightById), new { id = createdFlight.Id }, createdFlight);
            }

            return BadRequest(errorMessage);
        }

        [HttpPut("UpdateFlight/{id}")]
        public IActionResult UpdateFlight(int id, [FromBody] UpdateFlightVM request)
        {
            var result = _flightService.Update(id, request, out string errorMessage);
            if (result)
            {
                var updatedFlight = _flightService.GetById(id, out _);
                return Ok(updatedFlight);
            }

            return BadRequest(errorMessage);
        }

        [HttpDelete("DeleteFlight/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var result = _flightService.Delete(id, out string errorMessage);
            if (result)
                return NoContent();

            return BadRequest(errorMessage);
        }
    }
}
