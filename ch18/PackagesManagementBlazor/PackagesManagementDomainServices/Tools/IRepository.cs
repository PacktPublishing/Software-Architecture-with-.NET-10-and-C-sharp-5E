
namespace DDD.DomainLayer
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
    
}
