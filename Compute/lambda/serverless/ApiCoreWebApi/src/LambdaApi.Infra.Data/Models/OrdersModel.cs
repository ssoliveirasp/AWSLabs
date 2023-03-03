using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaApi.Infra.Data.DynamoDb.Models
{
    [DynamoDBTable("OrdersLambda")]
    public abstract class OrdersModel
    {
        private string _identifier = "";
        public static string GetIdentifier(string id) => $"USER#{id}";

        [DynamoDBHashKey("Id")]
        public string Identifier { get => GetIdentifier(_identifier); set { _identifier = value; } }
    }
}
