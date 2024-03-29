namespace MeetupMembersApi.Mongo
{
    public interface IDataRepository<T>
    {
        Task Create(T objectToSave, CancellationToken cancellationToken);
        Task<T?> Read(string id, CancellationToken cancellationToken);
        Task<T> Update(T objectToUpdate, CancellationToken cancellationToken);
        Task<bool> Delete(string id, CancellationToken cancellationToken);
        Task<List<T>> ReadAll(CancellationToken cancellationToken);
    }
}