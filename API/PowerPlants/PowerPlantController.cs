using Application.PowerPlants;
using Application.PowerPlants.Requests;
using Application.PowerPlants.Responses;
using Domain.PowerPlants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/power-plant")]
    public class PowerPlantController : ControllerBase
    {
        private readonly IPowerPlantRepository _powerPlantRepository;
        private readonly PowerPlantMapper _powerPlantMapper;

        public PowerPlantController(IPowerPlantRepository powerPlantRepository, PowerPlantMapper powerPlantMapper)
        {
            _powerPlantRepository = powerPlantRepository;
            _powerPlantMapper = powerPlantMapper;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<PowerPlantResponse>> Get(int id)
        {
            if (id < 1) return BadRequest(new { error = "Id not greter than 0!" });

            var powerPlant = await _powerPlantRepository.GetById(id);
            if (powerPlant == null) return NotFound(null);

            var response = _powerPlantMapper.FromModelToResponse(powerPlant);

            return Ok(response);
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] PowerPlantUpsertRequest powerPlantRequest)
        {
            var powerPlant = _powerPlantMapper.FromRequestToModel(powerPlantRequest);
    
            await _powerPlantRepository.Add(powerPlant);

            return CreatedAtAction(nameof(Get), new { id = powerPlant.Id }, new { id = powerPlant.Id });
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] PowerPlantUpsertRequest powerPlantRequest)
        {
            var powerPlant = _powerPlantMapper.FromRequestToModel(powerPlantRequest);
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

            return Ok(new { message = "Power Plant deleted" });
        }
    }
}
