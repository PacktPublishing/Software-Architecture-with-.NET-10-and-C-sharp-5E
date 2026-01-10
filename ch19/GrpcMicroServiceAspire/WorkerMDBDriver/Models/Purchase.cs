using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerMDomainModel.Models;

namespace WorkerMDBDriver.Models
{
    [Index(nameof(Time))]
    [Index(nameof(PurchaseTime))]
    [Index(nameof(Location))]
    public class Purchase:IPurchase
    {
        public Guid Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public DateTimeOffset PurchaseTime { get; set; }
        [MaxLength(128), Required]
        public required string Location { get; set; }
        public decimal Cost { get; set; }
        
    }
}
