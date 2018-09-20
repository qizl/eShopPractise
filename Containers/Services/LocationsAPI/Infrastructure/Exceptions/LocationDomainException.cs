using System;

namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class LocationDomainException : Exception
    {
        public LocationDomainException()
        { }

        public LocationDomainException(string message)
            : base(message)
        { }

        public LocationDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
