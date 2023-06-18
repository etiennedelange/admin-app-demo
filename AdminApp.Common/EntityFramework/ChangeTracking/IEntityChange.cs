namespace AdminApp.Common.EntityFramework.ChangeTracking
{
    public interface IEntityChange
    {
        long EntityId { get; }
        string EntityDescription { get; }
        string EntityName { get; }
    }
}
