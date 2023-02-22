using lambdaApi.Domain.Interface.Infra.Database;
using System.Collections;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

namespace LambdaApi.Infra.Data
{
    public class DynamoDbBaseRepository <T>: IDynamoDbBaseRepository<T>
    {

        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public DynamoDbBaseRepository()
        {
            _client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast2);
            _context = new DynamoDBContext(_client);
        }

        public async Task<T> QueryAsync(string tableName, object id)
        {
             return await _context.LoadAsync<T>(id, new DynamoDBOperationConfig { OverrideTableName = tableName });
        }

        public async Task<T> QueryAsync(object id)
        {
            return await _context.LoadAsync<T>(id);
        }
    }
}