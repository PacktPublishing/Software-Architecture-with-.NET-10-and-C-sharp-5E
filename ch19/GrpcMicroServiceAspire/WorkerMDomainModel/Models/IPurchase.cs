using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public interface IPurchase
    {
        Guid Id { get; set; }
        DateTimeOffset Time { get; set; }
        DateTimeOffset PurchaseTime { get; set; }
        public string Location { get; set; }
        public decimal Cost { get; set; }
    }
}
