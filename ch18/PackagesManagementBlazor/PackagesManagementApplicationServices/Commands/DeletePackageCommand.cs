using DDD.ApplicationLayer;

namespace PackagesManagementApplicationServices.Commands
{
    public class DeletePackageCommand(int id) : ICommand
    {
        public int PackageId { get; private set; } = id;
    }
}
