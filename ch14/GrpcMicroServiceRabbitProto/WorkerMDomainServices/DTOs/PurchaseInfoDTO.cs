using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerMDomainServices.DTOs
{
    public class PurchaseInfoDTO
    {
        public DateTimeOffset Time { get; set; }
        public DateTimeOffset PurchaseTime { get; set; }
        public required string Location { get; set; }
        public decimal Cost { get; set; }
        public Guid MessageId { get; set; }
    }
}
