using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public  class DayTotalAggregate(IDayTotal state)
    {
        public DateTimeOffset Id => state.Id;
        public decimal Total => state.Total;
        public void Add(decimal purchaseCost)
        {
            state.Total += purchaseCost;
        }
        public void Reset() 
        {
            state.Total = 0m;
        }
    }
}
