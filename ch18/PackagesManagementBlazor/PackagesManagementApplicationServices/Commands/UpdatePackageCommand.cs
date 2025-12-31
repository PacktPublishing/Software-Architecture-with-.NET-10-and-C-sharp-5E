using DDD.ApplicationLayer;


namespace PackagesManagementApplicationServices.Commands
{
    public class UpdatePackageCommand : ICommand
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public decimal Price { get; init; }
        public int DurationInDays { get; init; }
        public DateTime? StartValidityDate { get; init; }
        public DateTime? EndValidityDate { get; init; }
    }
}
