using Application.PowerProductions;
using Application.PowerProductions.Requests;
using Application.PowerProductions.Responses;
using Application.Services;
using Application.Services.VisualCrossing;
using Application.Services.VisualCrossing.Requests;
using Domain.Enums;
using Domain.PowerPlants;
using Domain.PowerProductions;
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
        private readonly PowerProductionMapper _powerProductionMapper;

        public PowerProductionController(IPowerProductionRepository powerProductionRepository, IPowerPlantRepository powerPlantRepository,
            TimeseriesService timeseriesService, IVcAPIClient vcAPIClient, IVcForecastService vcForecastService, PowerProductionMapper powerProductionMapper)
        {
            _powerProductionRepository = powerProductionRepository;
            _powerPlantRepository = powerPlantRepository;
            _timeseriesService = timeseriesService;
            _vcAPIClient = vcAPIClient;
            _vcForecastService = vcForecastService;
            _powerProductionMapper = powerProductionMapper;
        }

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<PowerProductionTimeseriesResponse>> GetTimeseries([FromQuery] PowerProductionTimeseriesRequest request)
        {
            var powerPlant = await _powerPlantRepository.GetById(request.PowerPlantId);
            if (powerPlant == null) return NotFound("Power Plant not found.");

            var response = _powerProductionMapper.FromTimeseriesRequestToResponse(request);

            switch (request.TimeseriesType)
            {
                case TimeseriesType.RealProduction:
                    var query = _powerProductionMapper.FromTimeseriesRequestToQuery(request);

                    var powerProductions = await _powerProductionRepository.GetTimeseries(query);
                    if (powerProductions.Count() == 0) return NotFound("No Power Production for this Power Plant.");

                    powerProductions = _timeseriesService.HandleGranularity(powerProductions.ToList(), request.Granularity);

                    response = _powerProductionMapper.AddRealProductionTimeseriesToResponse(response, powerProductions);
                    break;

                case TimeseriesType.ForecastedProduction:
                    var forecastData = await _vcAPIClient.GetGeospatialWeatherData(
                        new VcGeospatialWeatherRequest(powerPlant.Location.Latitude, powerPlant.Location.Longitude, request.Start, request.End)
                        );

                    if (forecastData == null) return NotFound("Forecast data API returned zero elements for a given period");

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
