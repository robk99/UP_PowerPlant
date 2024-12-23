using Application.Services.VisualCrossing.Requests;
using Application.Services.VisualCrossing.Responses;

namespace Application.Services.VisualCrossing
{
    public interface IVcAPIClient
    {
        /// <summary>
        /// Gets the geospatial weather data from the Visual Crossing API.
        /// Returns minimum of 1 hour granularity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>a JsonDocument - example in: VSGeospatialResponseExample.json</returns>
        Task<VcGeospatialWeatherResponse?> GetGeospatialWeatherData(VcGeospatialWeatherRequest request);
    }
}
