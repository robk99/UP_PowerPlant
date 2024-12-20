using System.ComponentModel.DataAnnotations;

namespace Domain.Locations
{
    public class Location(double latitude, double longitude)
    {
        [Required]
        public double Latitude { get; set; } = latitude;

        [Required]
        public double Longitude { get; set; } = longitude;
    }
}
