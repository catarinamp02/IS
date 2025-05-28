using RabbitMQ.Client;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using ProductionLine;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

//queue
await channel.ExchangeDeclareAsync(exchange: "productionLine", type: ExchangeType.Topic);


//criara straem
var streamSystem = await StreamSystem.Create(new StreamSystemConfig());

await streamSystem.CreateStream(new StreamSpec("production-stream")
{
    MaxLengthBytes = 5_000_000_000
});

var producer = await Producer.Create(new ProducerConfig(streamSystem, "production-stream"));



string routingKey;

Random rand = new();

while (true)
{
    var peca = new Peca();

    //converter o objeto em JSON
    string message = JsonSerializer.Serialize(peca);
    //converter JSON para bytes
    var body = Encoding.UTF8.GetBytes(message);

    //publicar no tópico em função do resultado do teste
    if (peca.resultadoTeste >= 2 && peca.resultadoTeste <= 5)
    {
        routingKey = "dados.producao.falha";
    }
    else if (peca.resultadoTeste == 1)
    {
        routingKey = "dados.producao.sucesso";
    }
    else
    {
        routingKey = "dados.producao.desconhecido";
    }

    //enviar para as filas
    await channel.BasicPublishAsync(exchange: "productionLine", routingKey: routingKey, body: body);

    Console.WriteLine($"Sent '{routingKey}':'{message}'");

    //enviar para a stream
    await producer.Send(new Message(body));

    Console.WriteLine($"Sent to stream :'{message}'");

    //Tempo de produção em ms
    int tempo = peca.tempoProd * 1000;

    //simular tempo de produção
    Thread.Sleep(tempo);

}
