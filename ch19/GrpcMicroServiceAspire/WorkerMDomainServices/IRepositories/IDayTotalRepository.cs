using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDomainModel.Models;

namespace WorkerMDomainServices.IRepositories
{
    public interface IDayTotalRepository: IRepository
    {
        DayTotalAggregate New(DateTimeOffset day);
        Task<DayTotalAggregate?> Get(DateTimeOffset day);
    }
}
