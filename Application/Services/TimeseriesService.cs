using Domain.Enums;
using Domain.PowerProductions;

namespace Application.Services
{
    public class TimeseriesService
    {
        public List<PowerProduction> HandleGranularity(List<PowerProduction> data, TimeseriesGranularity granularity)
        {
            switch (granularity)
            {
                case TimeseriesGranularity.FifteenMinutes:
                    return data;
                case TimeseriesGranularity.OneHour:
                    return GetHourlyData(data);
                
                default:
                    return data;
            }
        }

        private List<PowerProduction> GetHourlyData(List<PowerProduction> data)
        {
            var hourlyData = new List<PowerProduction>();
            var groupedData = data
                .GroupBy(d => new DateTime(d.Timestamp.Year, d.Timestamp.Month, d.Timestamp.Day, d.Timestamp.Hour, 0, 0)) // Group by the hour
                .ToList();

            var powerPlantId = data.First().PowerPlantId;

            foreach (var group in groupedData)
            {
                var totalPowerProduced = group.Sum(d => d.PowerProduced);
                hourlyData.Add(new PowerProduction(totalPowerProduced, group.Key, powerPlantId));
            }

            return hourlyData;
        }
    }

}
