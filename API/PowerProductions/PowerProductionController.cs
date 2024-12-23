using API.PowerProductions.Requests;
using API.PowerProductions.Responses;
using Application.Services;
using Application.Services.VisualCrossing;
using Application.Services.VisualCrossing.Requests;
using Domain.Enums;
using Domain.PowerPlants;
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
        private readonly IPowerPlantRepository _powerPlantRepository;
        private readonly TimeseriesService _timeseriesService;
        private readonly IVcAPIClient _vcAPIClient;
        private readonly IVcForecastService _vcForecastService;

        public PowerProductionController(IPowerProductionRepository powerProductionRepository, IPowerPlantRepository powerPlantRepository,
            TimeseriesService timeseriesService, IVcAPIClient vcAPIClient, IVcForecastService vcForecastService)
        {
            _powerProductionRepository = powerProductionRepository;
            _powerPlantRepository = powerPlantRepository;
            _timeseriesService = timeseriesService;
            _vcAPIClient = vcAPIClient;
            _vcForecastService = vcForecastService;
        }

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<PowerProductionTimeseriesResponse>> GetTimeseries([FromQuery] PowerProductionTimeseriesRequest request)
        {
            var response = new PowerProductionTimeseriesResponse
            {
                PowerPlantId = request.PowerPlantId,
                Start = request.Start.ToString(),
                End = request.End.ToString(),
            };

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
                    if (powerProductions.Count() == 0) return NotFound("No Power Production or Power Plant doesn't exist.");

                    powerProductions = _timeseriesService.HandleGranularity(powerProductions.ToList(), request.Granularity);

                    // TODO: Centralize mapping
                    foreach (var pp in powerProductions)
                    {
                        response.ProductionTimeseries.Add(new PowerProductionData() 
                            { Timestamp = pp.Timestamp.ToString(), PowerProduced = pp.PowerProduced }
                        );
                    }
                    break;

                case TimeseriesType.ForecastedProduction:

                    var forecastData = await _vcAPIClient.GetGeospatialWeatherData(
                        new VcGeospatialWeatherRequest(38.9697, -77.385, request.Start, request.End)
                        );

                    if (forecastData == null) return NotFound("Forecast data API returned zero elements for a given period");

                    var powerPlant = await _powerPlantRepository.GetById(request.PowerPlantId);
                    if (powerPlant == null) return NotFound("Power Plant not found.");

                    // TODO: Centralize mapping
                    foreach (var day in forecastData.Days)
                    {
                        foreach (var hour in day.Hours)
                        {
                            var dateTime = DateTimeOffset.FromUnixTimeSeconds(hour.DatetimeEpoch).UtcDateTime.ToLocalTime();
                            if (dateTime < request.Start || dateTime > request.End) continue;

                            response.ProductionTimeseries.Add(new PowerProductionData()
                                { 
                                    Timestamp = dateTime.ToString(),
                                    PowerProduced = _vcForecastService.GetForecastedProduction(hour.Cloudcover, powerPlant)
                                }
                            );
                        }
                    }

                    break;

                default:
                    // This shouldn't happen because the request should be validated
                    break;
            }

            return Ok(response);
        }
    }
}
