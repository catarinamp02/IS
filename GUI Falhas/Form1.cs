using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace GUI_Falhas
{
    public partial class Form1 : Form
    {
        private IConnection _connection;
        private IChannel _channel;
        public Form1()
        {
            InitializeComponent();
            StartConsumerAsync();
        }

        private async Task StartConsumerAsync()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();


            await _channel.ExchangeDeclareAsync(exchange: "productionLine", type: ExchangeType.Topic);

            //Fila para todos os dados de produção
            await _channel.QueueDeclareAsync(
                queue: "DadosProd",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            ////Fila para peça com falhas
            //await channel.QueueDeclareAsync(
            //    queue: "FalhasProd",
            //    durable: true,
            //    exclusive: false,
            //    autoDelete: false,
            //    arguments: null
            //);

            //Fila que recebe o tópico com todos os dados
            await _channel.QueueBindAsync(queue: "DadosProd", exchange: "productionLine", routingKey: "dados.producao.#");

            ////Fila que recebe os tópicos de falha e desconhecido
            //await channel.QueueBindAsync(queue: "FalhasProd", exchange: "productionLine", routingKey: "dados.producao.falha");
            //await channel.QueueBindAsync(queue: "FalhasProd", exchange: "productionLine", routingKey: "dados.producao.desconhecido");


            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Peca peca = JsonSerializer.Deserialize<Peca>(message);

                // Atualiza o componente da UI com thread-safe
                this.Invoke((MethodInvoker)delegate
                {
                    DataProd.Text = peca.dataProd.ToString();
                    HoraProd.Text = peca.horaProd.ToString();
                    Codigo.Text = peca.codigo;
                    TempoProd.Text = peca.tempoProd.ToString();
                    ResultadoTeste.Text = peca.resultadoTeste.ToString();
                    DataTeste.Text = peca.datateste.ToString();
                });

                return Task.CompletedTask;
            };

            await _channel.BasicConsumeAsync(queue: "DadosProd", autoAck: true, consumer: consumer);

            // Mantém a thread viva
            //while (true) Thread.Sleep(1000);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.OnFormClosed(e);
        }

    }
}
