using System.Collections;

namespace lambdaApi.Domain.Interface.Infra.Database
{
    public interface IDynamoDbProvider<T, C>
    {
        Task<T> QueryAsync(object haskKey, object rangeKey);
        Task<IEnumerable<T>> ScanAsync<T>(List<C> conditions);
        Task CreateTableFromAssembly();
        Task DeleteTable<T>();
        Task SaveAsync(T item);
        Task SaveAsync(T item, string tableName);
    }
}
