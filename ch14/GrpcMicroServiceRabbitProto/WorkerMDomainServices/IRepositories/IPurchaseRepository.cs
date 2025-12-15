using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDomainModel.Models;
using WorkerMDomainServices.DTOs;

namespace WorkerMDomainServices.IRepositories
{
    public interface IPurchaseRepository: IRepository
    {
        PurchaseAggregate New(PurchaseInfoDTO message);
        Task<PurchaseAggregate?> Get(Guid messageId);
    }
}
