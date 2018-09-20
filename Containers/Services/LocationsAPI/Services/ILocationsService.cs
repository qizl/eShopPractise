namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Services
{
    using Model;
    using ViewModel;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocationsService
    {
        Task<Locations> GetLocation(int locationId);

        Task<UserLocation> GetUserLocation(string id);

        Task<List<Locations>> GetAllLocation();

        Task<bool> AddOrUpdateUserLocation(string userId, LocationRequest locRequest);
    }
}
