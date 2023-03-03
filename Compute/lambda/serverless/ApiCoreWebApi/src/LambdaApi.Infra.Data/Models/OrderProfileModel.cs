using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaApi.Infra.Data.DynamoDb.Models
{
    public class OrdersProfileModel: OrdersModel
    {
        public const String SortKeyName = "#PROFILE";

        [DynamoDBRangeKey("Sk")]
        public string SortKey { get; set; } = SortKeyName;

        [DynamoDBProperty("Username")]
        public string Username { get; set; }

        [DynamoDBProperty("Addresses")]
        public string Addresses { get; set; }
    }
}
