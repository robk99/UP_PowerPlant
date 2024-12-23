namespace Application.Services.VisualCrossing.Requests
{
    public class VcGeospatialWeatherRequest(double latitude, double longitude, DateTime startDate, DateTime endDate)
    {
        public double Latitude { get; set; } = latitude;
        public double Longitude { get; set; }= longitude;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;

    }
}
