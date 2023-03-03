using lambdaApi.Domain.Entities;
using lambdaApi.Domain.Entities.Orders;
using lambdaApi.Domain.Enums;
using lambdaApi.Domain.Interface.Infra.Database;
using LambdaApi.Infra.Data.DynamoDb.Base;
using LambdaApi.Infra.Data.DynamoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LambdaApi.Infra.Data.DynamoDb.Seed
{
    public class OrdersProfileSeed : IOrdersSeed
    {
        IOrdersProfileRepository _profileRepository;

        public OrdersProfileSeed(IOrdersProfileRepository ordersRepository)
        {
            _profileRepository = ordersRepository;
        }

        public async void SeedOrdersProfiles()
        {
            var orderList = new List<ProfileOrders>();


            orderList.Add(new ProfileOrders() { UserName = "sarah", Addresses = GetAddress(new List<EnumsApi.AddressOrdersType>() { EnumsApi.AddressOrdersType.Home }) });
            orderList.Add(new ProfileOrders() { UserName = "junior", Addresses = GetAddress(new List<EnumsApi.AddressOrdersType>() { EnumsApi.AddressOrdersType.Bussiness }) });
            orderList.Add(new ProfileOrders() { UserName = "hannah", Addresses = GetAddress(new List<EnumsApi.AddressOrdersType>() { EnumsApi.AddressOrdersType.Home, EnumsApi.AddressOrdersType.Bussiness }) });

            foreach (var order in orderList)
            { 
                await _profileRepository.SaveAsync(order);
            }
        }

        private List<AddressOrders> GetAddress(List<EnumsApi.AddressOrdersType> addressOrdersType)
        {
            var address = new List<AddressOrders>();
            
            if (addressOrdersType.Contains(EnumsApi.AddressOrdersType.Home))
               address.Add(new AddressOrders() { Name = EnumsApi.AddressOrdersType.Home.ToString() , StreetAddress = "Rua X, 10", State = "Rio de Janeiro" });

            if (addressOrdersType.Contains(EnumsApi.AddressOrdersType.Bussiness))
                address.Add(new AddressOrders() { Name = EnumsApi.AddressOrdersType.Bussiness.ToString(), StreetAddress = "Rua Floriano, 304 ap 302", State = "Rio de Janeiro" });

            return address;
        }
    }
}
