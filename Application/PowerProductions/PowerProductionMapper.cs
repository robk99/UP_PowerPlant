using Application.PowerProductions.Requests;
using Application.PowerProductions.Responses;
using Domain.PowerProductions;
using Domain.PowerProductions.Queries;

namespace Application.PowerProductions
{
    public class PowerProductionMapper
    {
        public PowerProductionTimeseriesResponse FromTimeseriesRequestToResponse(PowerProductionTimeseriesRequest request)
        {
            return new PowerProductionTimeseriesResponse
            {
                PowerPlantId = request.PowerPlantId,
                Start = request.Start.ToString(),
                End = request.End.ToString(),
            };
        }

        public PowerProductionTimeseriesResponse AddRealProductionTimeseriesToResponse(PowerProductionTimeseriesResponse response, IEnumerable<PowerProduction> powerProductions)
        {
            foreach (var pp in powerProductions)
            {
                response.ProductionTimeseries.Add(new PowerProductionData()
                { Timestamp = pp.Timestamp.ToString(), PowerProduced = pp.PowerProduced }
                );
            }

            return response;
        }

        public PowerProductionTimeseriesQuery FromTimeseriesRequestToQuery(PowerProductionTimeseriesRequest request)
        {
            return new PowerProductionTimeseriesQuery
            {
                PowerPlantId = request.PowerPlantId,
                StartDate = request.Start,
                EndDate = request.End
            };
        }
    }
}
