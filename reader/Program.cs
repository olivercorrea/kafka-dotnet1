using System.Text.Json;
using Confluent.Kafka;
using Books;

var config = new ConsumerConfig
{
    BootstrapServers = "3.83.53.67:29092",
    GroupId = "reader"
};

var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("books");
Console.WriteLine("Subscribed to books.");

while (true)
{
    var consumed = consumer.Consume();
    var book = JsonSerializer.Deserialize<Book>(consumed.Message.Value)!;
    Console.WriteLine($"Consumed book '{book.Title}' by {book.Author} ({book.Year})");
}
