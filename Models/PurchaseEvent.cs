using System;
using System.Text.Json;
using Confluent.Kafka;

namespace KafkaProducer.Models
{
    public class PurchaseEvent
    {
        public long Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ItemPurchased { get; set; }
        public float Price { get; set; }
        //public override string ToString() => $"Id: {Id.ToString()}, Item: {ItemPurchased}, Price: {Price.ToString()}, TS: {TimeStamp.ToString()}";
    }
    public class PurchaseEventSerializer : ISerializer<PurchaseEvent>
    {
        public byte[] Serialize(PurchaseEvent data, SerializationContext context) 
            => JsonSerializer.SerializeToUtf8Bytes(data);
    }
}

