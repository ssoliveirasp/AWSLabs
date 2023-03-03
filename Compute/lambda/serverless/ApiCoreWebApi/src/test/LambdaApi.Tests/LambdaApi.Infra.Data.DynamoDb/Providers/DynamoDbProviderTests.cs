using lambdaApi.Domain.Entities;
using lambdaApi.Domain.Interface.Infra.Database;
using LambdaApi.Infra.Data.DynamoDb.Base;
using LambdaApi.Infra.Data.DynamoDb.Models;
using LambdaApi.Infra.Data.DynamoDb.Repository;
using LambdaApi.Infra.Data.DynamoDb.Seed;
using Xunit;
using static lambdaApi.Domain.Enums.EnumsApi;

namespace LambdaApi.Tests.LambdaApi.Infra.Data.DynamoDb.Repository
{
    public class DynamoDbProviderTests
    {
        IOrdersProfileRepository _profileRepository;
        IOrdersSeed _ordersSeed;

        public DynamoDbProviderTests()
        {
            _profileRepository = new OrdersProfileRepository(new DynamoDbProvider<OrdersProfileModel>());
            _ordersSeed = new OrdersProfileSeed(_profileRepository);

            _profileRepository.CreateTableFromAssembly().ConfigureAwait(false);
        }

        [Fact]
        public  void DynamoDbSeed()
        {
            _ordersSeed.SeedOrdersProfiles();
        }

        [Fact]
        public async void DynamoDbSearch()
        {
           var profileOrders =  await _profileRepository.SearchProfileByIdAsync(OrdersModel.GetIdentifier("sarah"));

            Assert.NotNull(profileOrders);
            Assert.NotNull(profileOrders.Addresses);
        }

        [Fact]
        public async void DynamoDbScan()
        {
            var profileOrders = await _profileRepository.SearchAllProfileByAddressOrdersType(new List<AddressOrdersType> { AddressOrdersType.Bussiness });

            Assert.NotNull(profileOrders);
        }

        [Fact]
        public async void DeleteTable()
        {
            await _profileRepository.DeleteTable();
        }
    }
}
