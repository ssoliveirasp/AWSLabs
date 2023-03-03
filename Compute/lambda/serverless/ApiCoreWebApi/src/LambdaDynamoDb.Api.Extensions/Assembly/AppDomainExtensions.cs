using Amazon.DynamoDBv2.DataModel;
using LambdaDynamoDb.Api.Extensions.Model;
using System.Reflection;

namespace LambdaDynamoDb.Api.Extensions
{
    public static class AppDomainExtensions
    {
        public static string GetTableName(this AppDomain currentDomain, Type type)
        {
            var tableAttribute = type.GetCustomAttributes(typeof(DynamoDBTableAttribute), true).Cast<DynamoDBTableAttribute>().FirstOrDefault();

            return tableAttribute.TableName;
        }

        public static List<DynamoDbClassAttributes> GetAssemblyDynamoDBTableAttributes(this AppDomain currentDomain, Assembly executingAssembly)
        {
            var tableDefinitions = new List<DynamoDbClassAttributes>();
            var types = executingAssembly.GetTypes().Where(t => t.IsDefined(typeof(DynamoDBTableAttribute)));

            foreach (var entidade in types)
            {
                var tableAttribute = entidade.GetCustomAttributes(typeof(DynamoDBTableAttribute), true).Cast<DynamoDBTableAttribute>().FirstOrDefault();
                var hashProperty = GetDynamoKeys<DynamoDBHashKeyAttribute>(entidade);
                var rangeProperty = GetDynamoKeys<DynamoDBRangeKeyAttribute>(entidade);
                var hashAttribute = hashProperty?.GetCustomAttributes(typeof(DynamoDBHashKeyAttribute), true).Cast<DynamoDBHashKeyAttribute>().FirstOrDefault();
                var rangeAttribute = rangeProperty?.GetCustomAttributes(typeof(DynamoDBRangeKeyAttribute), true).Cast<DynamoDBRangeKeyAttribute>().FirstOrDefault();

                var item = new DynamoDbClassAttributes()
                {
                    TableAttribute = tableAttribute,
                    HashKeyProperty = hashProperty,
                    HashKeyAttribute = hashAttribute,
                    RangeProperty = rangeProperty,
                    RangeAttribute = rangeAttribute
                };

                tableDefinitions.Add(item);
            }

            return tableDefinitions;
        }

        private static PropertyInfo? GetDynamoKeys<T>(Type? entidade)
        {
            var entity = (TypeInfo)entidade;
            var haskKey = entity.DeclaredProperties.FirstOrDefault(x => x.IsDefined(typeof(T)));

            if (haskKey == null && entidade.BaseType != null)
            {
                haskKey = ((TypeInfo)entity.BaseType).DeclaredProperties.FirstOrDefault(x => x.IsDefined(typeof(T)));
            }

            return haskKey;
        }
    }
}
