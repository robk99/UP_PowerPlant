namespace API.PowerPlants
{
    public abstract class BasePowerPlant
    {
        public string Name { get; set; }
        public float InstalledPower { get; set; }
        public DateTime InstallationDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
