using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerMDomainModel.Models
{
    public interface IDayTotal
    {
        DateTimeOffset Id { get; set; }
        decimal Total { get; set; }
    }
}
