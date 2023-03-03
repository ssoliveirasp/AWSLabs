using lambdaApi.Domain.Interface.Infra.Database;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using LambdaDynamoDb.Api.Extensions;
using System.Reflection;

namespace LambdaApi.Infra.Data.DynamoDb.Base
{
    public class DynamoDbProvider<T> : IDynamoDbProvider<T, ScanCondition>
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public DynamoDbProvider()
        {
            _client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast2);
            _context = new DynamoDBContext(_client);
        }

        public string GetTableName(Type model)
        {
            return AppDomain.CurrentDomain.GetTableName(model);
        }

        public async Task SaveAsync(T item)
        {
            await _context.SaveAsync(item);
        }

        public async Task SaveAsync(T item, string tableName)
        {
            await _context.SaveAsync(item, new DynamoDBOperationConfig { OverrideTableName = tableName });
        }

        public async Task<T> QueryAsync(object hashKey, object rangeKey)
        {
            var data = await _context.LoadAsync<T>(hashKey, rangeKey);

            return data;
        }
        public async Task<IEnumerable<T>> ScanAsync<T>(List<ScanCondition> conditions)
        {
           return await _context.ScanAsync<T>(conditions).GetRemainingAsync();
        }

        public async Task CreateTableFromAssembly()
        {
            var tableDefinitions = AppDomain.CurrentDomain.GetAssemblyDynamoDBTableAttributes(Assembly.GetExecutingAssembly());
            var queryTables = _client.ListTablesAsync().Result;

            foreach (var tableDefinition in tableDefinitions)
            {
                if (tableDefinition == null || queryTables.TableNames.Contains(tableDefinition.TableAttribute?.TableName))
                {
                    continue;
                }

                var request = new CreateTableRequest().GetRequestFromDynamoDBTableAttributes(tableDefinition);

                await _client.CreateTableAsync(request);
            }
        }

        public async Task DeleteTable<T>()
        {
            var request = new DeleteTableRequest(GetTableName(typeof(T)));
            var deleteResponse = await _client.DeleteTableAsync(request);
        }
    }
}