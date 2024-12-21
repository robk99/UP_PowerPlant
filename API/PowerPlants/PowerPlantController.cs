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
        public async Task<ActionResult<PowerPlant>> Get(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var powerPlant = await _powerPlantRepository.GetById(id);
            if (powerPlant == null) return NotFound(null);

            return Ok(powerPlant);
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] PowerPlant powerPlantDTO)
        {
            await _powerPlantRepository.Add(powerPlantDTO);

            return CreatedAtAction(nameof(Get), new { id = powerPlantDTO.Id });
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] PowerPlant powerPlantDTO)
        {
            var result = await _powerPlantRepository.Update(powerPlantDTO);
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
