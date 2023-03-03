using Amazon.DynamoDBv2.DataModel;
using lambdaApi.Domain.Interface.Infra.Database;
using LambdaApi.Infra.Data.DynamoDb.Base;
using LambdaApi.Infra.Data.DynamoDb.Models;
using LambdaApi.Infra.Data.DynamoDb.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaApi.Infra.Data.DynamoDb
{
    public static class RegisterDI
    {
        public static void RegisterDynamoDb(this IServiceCollection services)
        {
            services.AddScoped<DynamoDbProvider<OrdersProfileModel>, DynamoDbProvider<OrdersProfileModel>>();
            services.AddScoped<IOrdersProfileRepository, OrdersProfileRepository>();
        }
    }
}
