using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaProducer.Models;

namespace KafkaProducer
{
    class Program
    {
        public static async Task Main()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            var rand = new Random(47);

            // Create a producer that can be used to send messages to kafka that have no key and a value of type string 
            using var p = new ProducerBuilder<long, PurchaseEvent>(config)
                .SetValueSerializer(new PurchaseEventSerializer())
                .Build();

            var i = 0;
            while (true)
            {
                var purchaseEvent = new PurchaseEvent
                {
                    Id = ++i,
                    TimeStamp = System.DateTime.Now,
                    ItemPurchased = "Rand_Toy",
                    Price = rand.Next(),

                };
                // Construct the message to send (generic type must match what was used above when creating the producer)
                var message = new Message<long, PurchaseEvent>
                {
                    Key = purchaseEvent.Id,
                    Value = purchaseEvent
                };

                // Send the message to our test topic in Kafka                
                var dr = await p.ProduceAsync("test", message);
                Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset} at time {System.DateTime.Now}");

                Thread.Sleep(900);
            }
        }
    }
}
