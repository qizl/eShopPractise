using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;
using EnjoyCodes.eShopOnContainers.Services.MarketingAPI.Model;
using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.MarketingAPI.IntegrationEvents.Events
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
