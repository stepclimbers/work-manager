namespace WorkManager.Data.Models.Interfaces
{
    public interface IBaseEntity<TKey> : IAudit
    {
        TKey Id { get; set; }
    }
}
