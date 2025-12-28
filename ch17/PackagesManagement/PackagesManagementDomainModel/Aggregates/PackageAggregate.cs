using DDD.DomainLayer;
using PackagesManagementDomainModel.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public class PackageAggregate(IPackage state): Entity<int>
    {
        public override int Id => state.Id;
        public string Name { get {return state.Name; } 
            set { state.Name = value; } }
        public string? Description { get { return state.Description; } 
            set { state.Description = value; } }
        public decimal Price { get { return state.Price; } 
            set {
                if (value != state.Price)
                {
                    AddDomainEvent(new PackagePriceChangedEvent(
                            Id, value, EntityVersion, EntityVersion + 1));
                    state.EntityVersion = state.EntityVersion + 1;
                }
                state.Price = value; 
            } }
        public int DurationInDays { get { return state.DurationInDays; } 
            set { state.DurationInDays = value; } }
        public DateTime? StartValidityDate { get { return state.StartValidityDate; } 
            set { state.StartValidityDate = value; } }
        public DateTime? EndValidityDate { get { return state.EndValidityDate; } 
            set { state.EndValidityDate = value; } }
        public int DestinationId => state.DestinationId;
        public long EntityVersion => state.EntityVersion;
    }

}
