using DataTransmitter;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


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

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received:'{message}'\n");

    Peca peca = JsonSerializer.Deserialize<Peca>(message);

    await PostProduto(peca);

    await PostTeste(peca);
   
};

await channel.BasicConsumeAsync(queue: "DadosProd", autoAck: true, consumer: consumer);

await Task.Delay(-1);

async Task PostProduto (Peca peca)
    {
        //Criar objeto produto com base nos dados recebidos
        Produto produto = new Produto
        {
            codigo_Peca = peca.codigo,
            data_Producao = peca.dataProd.ToString(),
            hora_Producao = peca.horaProd.ToString(),
            tempo_Producao = peca.tempoProd,
        };

    //Converter em JSON
        var jsonProduto = JsonSerializer.Serialize(produto);

        var contentProduto = new StringContent(jsonProduto, Encoding.UTF8, "application/json");

        Console.WriteLine($"Produto: {jsonProduto}\n");

        //Fazer POST do produto
        var respostaProduto = await client.PostAsync("/api/Produtos", contentProduto);

        string responseContent = await respostaProduto.Content.ReadAsStringAsync();

        Console.WriteLine(await respostaProduto.Content.ReadAsStringAsync());
        Console.WriteLine("\n");

}

async Task PostTeste (Peca peca)
    {
        //Criar objeto Teste com base nos dados recebidos
        Teste teste = new Teste
        {
            iD_Produto = await getIdProduto(),
            codigo_Resultado = peca.resultadoTeste.ToString(),
            data_Teste = peca.datateste,
        };

        //Converter para JSON
        var jsonTeste = JsonSerializer.Serialize(teste);
        var contentTeste = new StringContent(jsonTeste, Encoding.UTF8, "application/json");

        Console.WriteLine($"Teste: {jsonTeste}\n");

        //Fazer POST do teste
        var respostaTeste = await client.PostAsync("/api/Testes", contentTeste);

        string responseContent = await respostaTeste.Content.ReadAsStringAsync();

        Console.WriteLine(await respostaTeste.Content.ReadAsStringAsync());

        Console.WriteLine("\n");
}
    async Task<int> getIdProduto()
    {
        try
        {
            var resposta = await client.GetAsync("/api/Produtos");

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