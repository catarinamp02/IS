using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Analytics_GUI
{
    public partial class Form1 : Form
    {
        int totalPecas = 0;
        int Total_pecas_OK = 0;
        int Total_pecas_com_falha = 0;
        int totalTempo = 0;
        public Form1()
        {
            InitializeComponent();
            StartConsumerAsync();
        }

        private async Task StartConsumerAsync()
        {
            var streamSystem = await StreamSystem.Create(new StreamSystemConfig());

            await streamSystem.CreateStream(new StreamSpec("production-stream")
            {
                MaxLengthBytes = 5_000_000_000
            });

            var consumer = await Consumer.Create(new ConsumerConfig(streamSystem, "production-stream")
            {
                OffsetSpec = new OffsetTypeFirst(),

                MessageHandler = async (stream, _, _, message) =>
                {
                    var recievedMessage = Encoding.UTF8.GetString(message.Data.Contents);

                    Peca peca = JsonSerializer.Deserialize<Peca>(recievedMessage);
                       

                    this.Invoke((MethodInvoker)delegate
                    {
                        //Total de peças;

                        totalPecas++;
                        TextBox_total_pecas.Text = totalPecas.ToString();

                        //Número total de peças com e sem falha

                        if(peca.resultadoTeste == 1)
                        {
                            Total_pecas_OK++;
                            TextBox_total_pecas_OK.Text = Total_pecas_OK.ToString(); 
                        }
                        else 
                        {
                            Total_pecas_com_falha++;
                            TextBox_total_pecas_falha.Text = Total_pecas_com_falha.ToString();
                        }

                        //Tempo médio de produção   

                        totalTempo += peca.tempoProd;
                        float mediaTempo = (float)totalTempo / totalPecas;

                        textBox_Tempo_Medio.Text = mediaTempo.ToString("F2");  


                    });

                    await Task.CompletedTask;
                }
            });
        }

    }
}
