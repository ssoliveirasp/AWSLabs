using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQS.Publisher 
{
  class Program 
  {
      
      static async Task Main(string[] args) 
      {
         Console.WriteLine("Publisher On");

         var client = new AmazonSQSClient(RegionEndpoint.SAEast1);
           var request = new SendMessageRequest
           {
                QueueUrl = "",
                MessageBody = "Teste"
           };

           await client.SendMessageAsync(request);
      }
  }
}
