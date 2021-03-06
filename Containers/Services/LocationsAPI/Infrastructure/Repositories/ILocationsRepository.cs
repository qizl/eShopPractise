﻿namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;
    using ViewModel;

    public interface ILocationsRepository
    {
        Task<Locations> GetAsync(int locationId);

        Task<List<Locations>> GetLocationListAsync();

        Task<UserLocation> GetUserLocationAsync(string userId);

        Task<List<Locations>> GetCurrentUserRegionsListAsync(LocationRequest currentPosition);

        Task AddUserLocationAsync(UserLocation location);

        Task UpdateUserLocationAsync(UserLocation userLocation);
    }
}
