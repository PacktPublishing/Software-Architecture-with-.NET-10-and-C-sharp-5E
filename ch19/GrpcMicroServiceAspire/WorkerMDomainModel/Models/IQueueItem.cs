using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public interface IQueueItem
    {
        long Id { get; set; }
        DateTimeOffset Time { get; set; }
        DateTimeOffset PurchaseTime { get; set; }
        DateTimeOffset ExtractionTime { get; set; }
        public string Location { get; set; }
        public decimal Cost { get; set; }
        public Guid MessageId { get; set; }
    }
}
