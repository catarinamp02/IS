using RabbitMQ.Client;
using ProductionLine;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "productionLine", type: ExchangeType.Topic);

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


    await channel.BasicPublishAsync(exchange: "productionLine", routingKey: routingKey, body: body);

    Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");

    //simular tempo de produção
    Thread.Sleep(rand.Next(5000, 21000));

}
