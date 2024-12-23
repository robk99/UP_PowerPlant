using Domain.Enums;

namespace Application.PowerProductions.Requests
{
    public record PowerProductionTimeseriesRequest
    {
        public int PowerPlantId { get; set; }
        public TimeseriesType TimeseriesType { get; set; }
        public TimeseriesGranularity Granularity { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
