using System.Collections;


namespace lambdaApi.Domain.Interface.Infra.Database
{
    public interface IDynamoDbBaseRepository<T>
    {
        Task<T> QueryAsync(object id);
        Task<T> QueryAsync(string tableName, object id);
    }
}
