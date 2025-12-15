using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerMDomainModel.Models;

namespace WorkerMDBDriver.Models
{
    public class DayTotal:IDayTotal
    {
        public DateTimeOffset Id { get; set; }
        public decimal Total { get; set; }
    }
}
