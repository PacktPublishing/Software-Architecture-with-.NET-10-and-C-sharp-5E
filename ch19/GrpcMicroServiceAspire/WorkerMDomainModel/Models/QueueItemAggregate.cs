using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public class QueueItemAggregate(IQueueItem state)
    {
        public long Id => state.Id;
        public DateTimeOffset Time => state.Time;
        public DateTimeOffset PurchaseTime => state.PurchaseTime;
        public DateTimeOffset ExtractionTime => state.ExtractionTime;
        public string Location => state.Location;
        public decimal Cost => state.Cost;
        public Guid MessageId => state.MessageId;
        public void MarkExtraction(DateTimeOffset time)
        {
            state.ExtractionTime = time;
        }
       
    }
}
