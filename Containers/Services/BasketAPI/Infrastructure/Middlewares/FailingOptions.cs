using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Infrastructure.Middlewares
{
    public class FailingOptions
    {
        public string ConfigPath = "/Failing";
        public List<string> EndpointPaths { get; set; } = new List<string>();
    }
}
