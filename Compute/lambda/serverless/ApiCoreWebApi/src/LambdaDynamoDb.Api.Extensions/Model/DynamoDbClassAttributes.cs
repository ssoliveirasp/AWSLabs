using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LambdaDynamoDb.Api.Extensions.Model
{
    public class DynamoDbClassAttributes
    {
        public DynamoDBTableAttribute? TableAttribute { get; set; }

        public PropertyInfo? HashKeyProperty { get; set; }
        public DynamoDBHashKeyAttribute? HashKeyAttribute { get; set; }

        public PropertyInfo? RangeProperty { get; set; }
        public DynamoDBRangeKeyAttribute? RangeAttribute { get; set; }
    }
}
