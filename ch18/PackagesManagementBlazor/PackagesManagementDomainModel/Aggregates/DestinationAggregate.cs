using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public class DestinationAggregate(IDestination state): Entity<int>
    {
        public override int Id => state.Id;
        public string Name => state.Name;
        public string Country => state.Country;
        public string? Description => state.Description;
        public void ChangeDescription(string newText) => state.Description=newText;

    }
}
