using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.SeedWork
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        private int _Id;
        public virtual int Id
        {
            get
            {
                return this._Id;
            }
            protected set
            {
                this._Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => this._domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            this._domainEvents = this._domainEvents ?? new List<INotification>();
            this._domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem) => this._domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() => this._domainEvents?.Clear();

        public bool IsTransient() => this.Id == default(Int32);

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = obj as Entity;
            if (item.IsTransient() || this.IsTransient())
                return false;

            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right) => !(left == right);
    }
}
