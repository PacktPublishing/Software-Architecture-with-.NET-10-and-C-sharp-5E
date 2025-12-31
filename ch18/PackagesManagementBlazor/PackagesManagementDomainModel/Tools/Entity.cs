using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.DomainLayer
{
    public abstract class Entity<K>
        where K: IEquatable<K>
    {

        public virtual K Id { get; protected set; } = default!;
        public bool IsTransient()
        {
            return Object.Equals(Id, default(K));
            
        }
        #region domain events handling
        public List<IEventNotification> DomainEvents { get; private set; } = null!;
        public void AddDomainEvent(IEventNotification evt)
        {
            DomainEvents ??= new List<IEventNotification>();
            DomainEvents.Add(evt);
        }
        public void RemoveDomainEvent(IEventNotification evt)
        {
            DomainEvents?.Remove(evt);
        }
        #endregion
        #region override Equal
        public override bool Equals(object? obj)
        {
            return obj != null && obj is Entity<K> entity &&
              Equals(entity); 
        }

        public bool Equals(Entity<K> other)
        {
            if (other.IsTransient() || this.IsTransient())
                return false;
            return Object.Equals(Id, other.Id);
        }

        int? _requestedHashCode;
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = HashCode.Combine(Id);
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity<K> left, Entity<K> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity<K> left, Entity<K> right)
        {
            return !(left == right);
        }
        #endregion

    }
}
