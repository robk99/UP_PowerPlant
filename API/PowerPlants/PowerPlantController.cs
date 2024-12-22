using API.PowerPlants.Requests;
using API.PowerPlants.Responses;
using Domain.PowerPlants;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/power-plant")]
    public class PowerPlantController : ControllerBase
    {
        private IPowerPlantRepository _powerPlantRepository { get; set; }
        public PowerPlantController(IPowerPlantRepository powerPlantRepository)
        {
            _powerPlantRepository = powerPlantRepository;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<PowerPlantResponse>> Get(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var powerPlant = await _powerPlantRepository.GetById(id);
            if (powerPlant == null) return NotFound(null);

            return Ok("asdf");
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] PowerPlantUpsertRequest powerPlantRequest)
        {
            var powerPlant = new PowerPlant(powerPlantRequest.InstalledPower, powerPlantRequest.InstallationDate, 
                new Domain.Locations.Location(powerPlantRequest.Latitude, powerPlantRequest.Longitude), powerPlantRequest.Name);

            await _powerPlantRepository.Add(powerPlant);

            return CreatedAtAction(nameof(Get), new { id = powerPlant.Id }, new { id = powerPlant.Id });
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] PowerPlantUpsertRequest powerPlantRequest)
        {
            var powerPlant = new PowerPlant(powerPlantRequest.InstalledPower, powerPlantRequest.InstallationDate,
                new Domain.Locations.Location(powerPlantRequest.Latitude, powerPlantRequest.Longitude), powerPlantRequest.Name);
            powerPlant.Id = (int)powerPlantRequest.Id;

            var result = await _powerPlantRepository.Update(powerPlant);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var result = await _powerPlantRepository.Delete(id);
            if (!result) return NoContent();

            return Ok("Power Plant deleted");
        }
    }
}