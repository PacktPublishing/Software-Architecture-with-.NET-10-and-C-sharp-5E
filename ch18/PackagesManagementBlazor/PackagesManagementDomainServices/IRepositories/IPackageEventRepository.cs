using DDD.DomainLayer;
using PackagesManagementDomainModel.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackagesManagementDomainServices.IRepositories
{
    public interface IPackageEventRepository:IRepository
    {
        Task<IEnumerable<PackageEventAggregate>> GetFirstNAsync(int n);
        PackageEventAggregate New(PackageEventType type, int id, long oldVersion, 
            long? newVersion= null, decimal price = 0);
    }
}
