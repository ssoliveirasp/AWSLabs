using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQS.Consumer
{
  class Program
  {
      const string _queueUrl = "https://sqs.us-west-2.amazonaws.com/878553792015/Teste";
      static async Task Main(string[] args)
      {
           Console.WriteLine("Hello World");

         var client = new AmazonSQSClient(RegionEndpoint.SAEast1);
           var request = new ReceiveMessageRequest()
           {
                QueueUrl = _queueUrl
           };

           var response = await client.ReceiveMessageAsync(request);

            foreach(var message in response.Messages) {
              await client.DeleteMessageAsync(_queueUrl, message.ReceiptHandle);
            }
      }
  }
}
