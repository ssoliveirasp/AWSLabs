using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using LambdaDynamoDb.Api.Extensions.Model;

namespace LambdaDynamoDb.Api.Extensions
{
    public static class CreateTableRequestExtensions
    {
        public static CreateTableRequest GetRequestFromDynamoDBTableAttributes(this CreateTableRequest request,DynamoDbClassAttributes tableDefinition)
        {
            return new CreateTableRequest
            {
                TableName = tableDefinition.TableAttribute?.TableName,
                AttributeDefinitions = new List<AttributeDefinition>
                        {
                            new AttributeDefinition
                            {
                                AttributeName = tableDefinition.HashKeyAttribute?.AttributeName,
                                AttributeType = "S"
                            },
                            new AttributeDefinition
                            {
                                AttributeName = tableDefinition.RangeAttribute?.AttributeName,
                                AttributeType = "S"
                            },
                        },
                KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement
                            {
                                AttributeName = tableDefinition.HashKeyAttribute?.AttributeName,
                                KeyType = "HASH"
                            },
                            new KeySchemaElement
                            {
                                AttributeName = tableDefinition.RangeAttribute?.AttributeName,
                                KeyType = "RANGE"
                            }
                        },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };
        }
    }
}
