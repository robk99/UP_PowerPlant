using API.PowerProductions.Requests;
using API.PowerProductions.Responses;
using Application.Services;
using Domain.Enums;
using Domain.PowerProductions;
using Domain.PowerProductions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/power-production")]
    public class PowerProductionController : ControllerBase
    {
        private readonly IPowerProductionRepository _powerProductionRepository;
        private readonly TimeseriesService _timeseriesService;

        public PowerProductionController(IPowerProductionRepository powerProductionRepository, TimeseriesService timeseriesService)
        {
            _powerProductionRepository = powerProductionRepository;
            _timeseriesService = timeseriesService;
        }

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<PowerProductionTimeseriesResponse>> GetTimeseries([FromQuery] PowerProductionTimeseriesRequest request)
        {
            switch (request.TimeseriesType)
            {
                case TimeseriesType.RealProduction:
                    // TODO: Centralize mapping
                    var query = new PowerProductionTimeseriesQuery
                    {
                        PowerPlantId = request.PowerPlantId,
                        StartDate = request.Start,
                        EndDate = request.End
                    };
                    var powerProductions = await _powerProductionRepository.GetTimeseries(query);
                    if (powerProductions.Count() == 0) return NotFound(null);

                    powerProductions = _timeseriesService.HandleGranularity(powerProductions.ToList(), request.Granularity);

                    // TODO: Centralize mapping
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
