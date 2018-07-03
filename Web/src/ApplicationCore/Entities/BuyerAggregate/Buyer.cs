using System.Collections.Generic;
using Ardalis.GuardClauses;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string IdentityGuid { get; set; }
        public List<PaymentMethod> _paymentMethod = new List<PaymentMethod>();

        public IEnumerable<PaymentMethod> paymentMethods => _paymentMethod.AsReadOnly();

        private Buyer() { }

        public Buyer(string identity) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            IdentityGuid = identity
        }
    }
}
