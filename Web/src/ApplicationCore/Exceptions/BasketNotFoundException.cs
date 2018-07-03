using System;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(string message) : base(message) { }

        public BasketNotFoundException(int basketId) : base($"No basket found with id {basketId}") { }

        public BasketNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected BasketNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
