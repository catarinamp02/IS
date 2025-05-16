using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "productionLine", type: ExchangeType.Topic);

//Fila para todos os dados da produção
await channel.QueueDeclareAsync(
    queue: "DadosProd",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

//Fila que recebe o tópico com todos os dados
await channel.QueueBindAsync(queue: "DadosProd", exchange: "productionLine", routingKey: "dados.producao.#");

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" [x] Received:'{message}'");

    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queue: "DadosProd", autoAck: true, consumer: consumer);
