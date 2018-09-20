using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;
using EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Model;
using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.IntegrationEvents.Events
{
    public class UserLocationUpdatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public List<UserLocationDetails> LocationList { get; set; }

        public UserLocationUpdatedIntegrationEvent(string userId, List<UserLocationDetails> locationList)
        {
            UserId = userId;
            LocationList = locationList;
        }
    }
}
