using Application.Services.VisualCrossing.Requests;
using Application.Services.VisualCrossing;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Application.Services.VisualCrossing.Responses;

namespace Infrastructure.Services.VisualCrossing
{
    public class VcAPIClient : IVcAPIClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        private readonly string _apiKey;
        private readonly string _baseUrl;

        public VcAPIClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

            _apiKey = _configuration["VisualCrossing:APIKey"]!;
            _baseUrl = _configuration["VisualCrossing:BaseUrl"]!;
        }


        public async Task<VcGeospatialWeatherResponse?> GetGeospatialWeatherData(VcGeospatialWeatherRequest request)
        {
            var endpointUrl = _baseUrl + $"/{request.Latitude},{request.Longitude}/" +
                $"{FormatDateForAPI(request.StartDate)}/{FormatDateForAPI(request.EndDate)}" +
                $"?unitGroup=metric&key={_apiKey}";

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync(endpointUrl);
                response.EnsureSuccessStatusCode();

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<VcGeospatialWeatherResponse>(contentStream);
                }
            }
        }

        private string FormatDateForAPI(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
