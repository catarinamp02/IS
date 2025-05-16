using DataTransmitter;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


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

using var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7289");

consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" [x] Received:'{message}'");

    Peca peca = JsonSerializer.Deserialize<Peca>(message);

    PostProduto(peca);

    PostTeste(peca);

    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queue: "DadosProd", autoAck: true, consumer: consumer);

    async Task PostProduto (Peca peca)
    {
        //Criar objeto produto com base nos dados recebidos
        Produto produto = new Produto
        {
            codigo_Peca = peca.codigo,
            data_Producao = peca.dataProd,
            hora_Producao = peca.horaProd,
            tempo_Producao = peca.tempoProd,
        };

        //Converter em JSON
        var jsonProduto = JsonSerializer.Serialize(produto);
        var contentProduto = new StringContent(jsonProduto, Encoding.UTF8, "application/json");

        //Fazer POST do produto
        var respostaProduto = await client.PostAsync("/api/Produto", contentProduto);
    }

    async Task PostTeste (Peca peca)
    {
        //Criar objeto Teste com base nos dados recebidos
        Teste teste = new Teste
        {
            iD_Produto = await getIdProduto(),
            codigo_Resultado = peca.resultadoTeste,
            data_Teste = peca.datateste,
        };

        //Converter para JSON
        var jsonTeste = JsonSerializer.Serialize(teste);
        var contentTeste = new StringContent(jsonTeste, Encoding.UTF8, "application/json");

        //Fazer POST do teste
        var respostaTeste = await client.PostAsync("/api/Testes", contentTeste);
    }
    async Task<int> getIdProduto()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7289");

        try
        {
            var resposta = await client.GetAsync("/api/produtos");

            //Verificar se a resposta foi de sucesso
            resposta.EnsureSuccessStatusCode();

            var stream = await resposta.Content.ReadAsStreamAsync();

            //converter lista de objetos JSON numa lista de objetos Produto
            var produtos = await JsonSerializer.DeserializeAsync<List<Produto>>(stream);

            if (produtos == null || produtos.Count == 0)
                return -1;

            // retorna o último produto
            var ultimoProduto = produtos[produtos.Count - 1];

            return ultimoProduto.iD_Produto;


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return -1; // Retorno em caso de erro
        }
    }