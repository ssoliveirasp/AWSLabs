using Amazon.DynamoDBv2.DataModel;
using lambdaApi.Domain.Entities.Orders;
using lambdaApi.Domain.Enums;
using lambdaApi.Domain.Interface.Infra.Database;
using LambdaApi.Infra.Data.DynamoDb.Base;
using LambdaApi.Infra.Data.DynamoDb.Models;
using System.Text.Json;

namespace LambdaApi.Infra.Data.DynamoDb.Repository
{
    public class OrdersProfileRepository : IOrdersProfileRepository
    {
        private readonly string _tableName = "";
        DynamoDbProvider<OrdersProfileModel> _provider;

        public OrdersProfileRepository(DynamoDbProvider<OrdersProfileModel> provider)
        {
            _provider = provider;
            _tableName = provider.GetTableName(typeof(OrdersProfileModel));
        }

        public async Task CreateTableFromAssembly()
        {
            await _provider.CreateTableFromAssembly();
        }

        public async Task SaveAsync(ProfileOrders item)
        {
            var model = new OrdersProfileModel()
            { 
                Identifier = item.UserName,
                Username = item.UserName,
                Addresses = JsonSerializer.Serialize<List<AddressOrders>>(item.Addresses),
            };
            await _provider.SaveAsync(model);

        }

        public async Task<IEnumerable<ProfileOrders>> SearchAllProfileByAddressOrdersType(List<EnumsApi.AddressOrdersType> typeAddress)
        {
            var conditions = new List<ScanCondition>();

            typeAddress.ForEach(x =>
            {
                conditions.Add(new ScanCondition("Addresses", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains, $"\"Name\":\"{x.ToString()}\""));
            });

            var items = await _provider.ScanAsync<OrdersProfileModel>(conditions);

            return MapDynamoToDomainItems(items);
        }

        public async Task<ProfileOrders> SearchProfileByIdAsync(string id)
        {
            try
            {
                var data = await _provider.QueryAsync(id, OrdersProfileModel.SortKeyName);

                return MapDynamoToDomainItem(data);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        private static List<ProfileOrders> MapDynamoToDomainItems(IEnumerable<OrdersProfileModel> itemsDynamoDb)
        {
            var items = new List<ProfileOrders>();

            itemsDynamoDb.ToList().ForEach(x => items.Add(MapDynamoToDomainItem(x)));

            return items;
        }

        private static ProfileOrders MapDynamoToDomainItem(OrdersProfileModel itemDynamoDb)
        {
            return new ProfileOrders()
            {
                UserName = itemDynamoDb.Username,
                Addresses = JsonSerializer.Deserialize<List<AddressOrders>>(itemDynamoDb.Addresses)
            };
        }

        public async Task DeleteTable()
        {
            await _provider.DeleteTable<OrdersProfileModel>();
        }
    }
}
