using API.PowerProductions.Requests;
using API.PowerProductions.Responses;
using Domain.Enums;
using Domain.PowerProductions;
using Domain.PowerProductions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/power-production")]
    public class PowerProductionController : ControllerBase
    {
        private IPowerProductionRepository _powerProductionRepository { get; set; }
        public PowerProductionController(IPowerProductionRepository powerProductionRepository)
        {
            _powerProductionRepository = powerProductionRepository;
        }

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<PowerProductionTimeseriesResponse>> GetTimeseries([FromQuery] PowerProductionTimeseriesRequest request)
        {
            switch (request.TimeseriesType)
            {
                case TimeseriesType.RealProduction:
                    // TODO: map with automapper PowerProductionTimeseriesRequest --> PowerProductionTimeseriesQuery
                    var query = new PowerProductionTimeseriesQuery
                    {
                        PowerPlantId = request.PowerPlantId,
                        StartDate = request.Start,
                        EndDate = request.End
                    };
                    var powerProductions = await _powerProductionRepository.GetTimeseries(query);
                    if (powerProductions.Count() == 0) return NotFound(null);

                    // TODO: map with automapper? List<PowerProduction> --> PowerProductionTimeseriesResponse
                    var response = new PowerProductionTimeseriesResponse
                    {
                        PowerPlantId = request.PowerPlantId,
                        Start = request.Start.ToString(),
                        End = request.End.ToString(),
                    };
                    foreach (var pp in powerProductions)
                    {
                        response.ProductionTimeseries.Add(new PowerProductionData() 
                            { Timestamp = pp.Timestamp.ToString(), PowerProduced = pp.PowerProduced }
                        );
                    }

                    // TODO: Handle granularity
                    return Ok(response);

                case TimeseriesType.ForecastedProduction:
                    break;

                default:
                    // This shouldn't happen because the request should be validated
                    break;
            }

            return Ok();
        }
    }
}
