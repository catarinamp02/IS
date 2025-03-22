using System.Text.Json;
using System.Text;

namespace InterfaceAPI
{
    public partial class Form : System.Windows.Forms.Form
    {
        private static readonly HttpClient client = new HttpClient();


        public Form()
        {
            InitializeComponent();
        }



        private async void button_Inserir_Produto_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var produto = new
                {
                    codigo_Peca = text_Codigo_Peca.Text,
                    data_Producao = text_Data_Producao.Text,
                    hora_Producao = text_Hora_Producao.Text,
                    tempo_Producao = int.Parse(text_Tempo_Producao.Text)
                };

                var json = JsonSerializer.Serialize(produto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                var response = await client.PostAsync("https://localhost:7289/api/Produtos", content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(responseContent, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        // Tenta interpretar o erro que vem da API como JSON
                        var erroObj = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

                        if (erroObj != null && erroObj.ContainsKey("erro"))
                        {
                            MessageBox.Show(erroObj["erro"], "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(responseContent, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        // Caso a resposta não seja JSON, exibe o erro como está
                        MessageBox.Show(responseContent, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro de conexão com o servidor: {ex.Message}", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Todos os campos são obrigatórios!", "Erro de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}