using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public class PurchaseAggregate(IPurchase state)
    {
        public Guid Id => state.Id;
        public DateTimeOffset Time => state.Time;
        public DateTimeOffset PurchaseTime => state.PurchaseTime;
        public string Location => state.Location;
        public decimal Cost => state.Cost;
    }
}
