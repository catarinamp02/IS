using System.Text.Json;
using System.Text;

namespace InterfaceAPI
{
    public partial class Form : System.Windows.Forms.Form
    {
        //private static readonly HttpClient client = new HttpClient();

        public Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(900, 100);
            _ = CarregarProdutosNoComboBox();
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
                    await CarregarProdutosNoComboBox();
                    comb_Cod_Produto.SelectedItem = comb_Cod_Produto.Items
                    .Cast<KeyValuePair<int, string>>()
                    .FirstOrDefault(p => p.Value == text_Codigo_Peca.Text);
                    MessageBox.Show(responseContent, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //LimparCampos();
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

        private async Task CarregarProdutosNoComboBox()
        {
            try
            {
                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                var response = await client.GetAsync("https://localhost:7289/api/Produtos");
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var produtos = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Garante que "iD_Produto" seja reconhecido corretamente
                    });

                    if (produtos != null && produtos.Any())
                    {
                        comb_Cod_Produto.Items.Clear(); // Limpa a ComboBox antes de adicionar novos itens

                        foreach (var produto in produtos)
                        {
                            // Verifica se as chaves existem no dicionário retornado pela API
                            if (produto.TryGetValue("iD_Produto", out JsonElement idProdutoElement) &&
                                produto.TryGetValue("codigo_Peca", out JsonElement codigoPecaElement))
                            {
                                int idProduto = idProdutoElement.GetInt32();
                                string codigoPeca = codigoPecaElement.GetString();

                                // Adiciona um item no formato KeyValuePair (ID, Código da Peça)
                                comb_Cod_Produto.Items.Add(new KeyValuePair<int, string>(idProduto, codigoPeca));
                            }
                        }

                        // Configura a ComboBox 
                        comb_Cod_Produto.DisplayMember = "Value";
                        comb_Cod_Produto.ValueMember = "Key";
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Erro ao procurar produtos: {responseContent}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comb_Cod_Produto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comb_Cod_Produto.SelectedItem is KeyValuePair<int, string> selectedProduto)
            {
                int idProdutoSelecionado = selectedProduto.Key;
                // MessageBox.Show($"Produto Selecionado: ID {idProdutoSelecionado}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void button_Inserir_Teste_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (comb_Cod_Produto.SelectedItem == null)
                {
                    MessageBox.Show("Selecione o Código da Peça!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var produtoSelecionado = (KeyValuePair<int, string>)comb_Cod_Produto.SelectedItem;

                if (string.IsNullOrWhiteSpace(text_Codigo_Resultado.Text) || string.IsNullOrWhiteSpace(text_Data_Teste.Text))
                {
                    MessageBox.Show("Todos os campos são obrigatórios!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var teste = new
                {
                    iD_Produto = produtoSelecionado.Key,
                    codigo_Resultado = text_Codigo_Resultado.Text,
                    data_Teste = text_Data_Teste.Text
                };

                var json = JsonSerializer.Serialize(teste);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                var response = await client.PostAsync("https://localhost:7289/api/Testes", content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(responseContent, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
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
        private void LimparCampos()
        {
            text_Codigo_Peca.Text = "";
            text_Data_Producao.Text = "";
            text_Hora_Producao.Text = "";
            text_Tempo_Producao.Text = "";
            text_Codigo_Resultado.Text = "";
            text_Data_Teste.Text = "";
            comb_Cod_Produto.SelectedIndex = -1; // Remove a seleção da ComboBox
        }
    }
}