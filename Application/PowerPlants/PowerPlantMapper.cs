using Application.PowerPlants.Requests;
using Application.PowerPlants.Responses;
using Domain.PowerPlants;

namespace Application.PowerPlants
{
    public class PowerPlantMapper
    {
        public PowerPlantResponse FromModelToResponse(PowerPlant powerPlant)
        {
            return new PowerPlantResponse()
            {
                Id = powerPlant.Id,
                Name = powerPlant.Name,
                InstallationDate = powerPlant.InstallationDate,
                InstalledPower = powerPlant.InstalledPower,
                Latitude = (double)powerPlant.Location?.Latitude,
                Longitude = (double)powerPlant.Location?.Longitude
            };
        }

        public PowerPlant FromRequestToModel(PowerPlantUpsertRequest request)
        {
            return new PowerPlant(request.InstalledPower, request.InstallationDate,
                new Domain.Locations.Location(request.Latitude, request.Longitude), request.Name);
        }
    }
}
