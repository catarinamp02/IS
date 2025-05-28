using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteSOAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in metodosSOAP.Keys)
                cbMetodo.Items.Add(item);

            cbMetodo.SelectedIndex = 0;
        }


        // Dicionário dos nomes dos métodos SOAP 
        private Dictionary<string, string> metodosSOAP = new Dictionary<string, string>
        {
            { "Custo Total num Período", "GetCustoTotalPeriodo" },
            { "Lucro Total num Período", "GetLucroTotalPeriodo" },
            { "Prejuízo por Peça num Período", "GetPrejuizoPorPecaPeriodo" },
            { "Peça com Maior Prejuízo", "GetPecaComMaiorPrejuizo" },
            { "Dados Financeiros por Peça", "GetFinanceiroPorPeca" }
        };

        // Métodos auxiliares para adicionar campos de data e hora
        private void AddDateTimeField(string label, string name, ref int posY)
        {
            pnlParametros.Controls.Add(new Label() { Text = label, Top = posY, Left = 10 });
            posY += 20;
            pnlParametros.Controls.Add(new DateTimePicker()
            {
                Name = name,
                Format = DateTimePickerFormat.Short,
                Top = posY + 5,
                Left = 10
            });
            posY += 35;
        }

        private void AddTimeField(string label, string name, ref int posY)
        {
            pnlParametros.Controls.Add(new Label() { Text = label, Top = posY, Left = 10 });
            posY += 20;
            pnlParametros.Controls.Add(new DateTimePicker()
            {
                Name = name,
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Top = posY + 5,
                Left = 10
            });
            posY += 35;
        }


        // Métodos auxiliares para obter valores Date e Time da interface
        private DateTime GetDate(string controlName) =>
              ((DateTimePicker)pnlParametros.Controls[controlName]).Value.Date;

        private TimeSpan GetTime(string controlName) =>
            ((DateTimePicker)pnlParametros.Controls[controlName]).Value.TimeOfDay;

   

        //evento para combobox de metodos
        private void cbMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlParametros.Controls.Clear();
            int posY = 30;
            string nome = cbMetodo.SelectedItem.ToString();
            string metodo = metodosSOAP[nome];

            if (metodo.Contains("Periodo"))
            {
                AddDateTimeField("Data Início", "dtInicio", ref posY);
                AddTimeField("Hora Início", "horaInicio", ref posY);
                AddDateTimeField("Data Fim", "dtFim", ref posY);
                AddTimeField("Hora Fim", "horaFim", ref posY);
            }
            else if (metodo == "GetFinanceiroPorPeca")
            {
                pnlParametros.Controls.Add(new Label() { Text = "Código da Peça", Top = posY, Left = 10 });
                posY += 20;
                pnlParametros.Controls.Add(new TextBox() { Name = "txtCodigoPeca", Top = posY+5, Left = 10, Width = 150 });
            }
        }



        // Evento do "botão Selecionar" para executar o método selecionado
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            var client = new FinanceiroWS.FinanceiroServiceSoapClient();
            string nome = cbMetodo.SelectedItem.ToString();
            string metodo = metodosSOAP[nome];
            textResultado.Clear();

            try
            {
                if (metodo == "GetCustoTotalPeriodo" || metodo == "GetLucroTotalPeriodo" || metodo == "GetPrejuizoPorPecaPeriodo")
                {
                    DateTime di = GetDate("dtInicio");
                    TimeSpan hi = GetTime("horaInicio");
                    DateTime df = GetDate("dtFim");
                    TimeSpan hf = GetTime("horaFim");

                    //aux 
                    DateTime dataHoraInicio = di + hi;
                    DateTime dataHoraFim = df + hf;
                    
                    if (dataHoraInicio > dataHoraFim)
                    {
                        textResultado.Text = "Erro: a data/hora inicial deve ser anterior ou igual à final.";
                        return;
                    }

                    string dataInicioStr = di.ToString("yyyy-MM-dd");
                    string horaInicioStr = hi.ToString(@"hh\:mm\:ss");
                    string dataFimStr = df.ToString("yyyy-MM-dd");
                    string horaFimStr = hf.ToString(@"hh\:mm\:ss");

                    if (metodo == "GetCustoTotalPeriodo")
                    {
                        var result = client.GetCustoTotalPeriodo(dataInicioStr, horaInicioStr, dataFimStr, horaFimStr);
                        textResultado.Text = $"Custo total: €{result.ToString("F2")}";
                    }
                    else if (metodo == "GetLucroTotalPeriodo")
                    {
                        var result = client.GetLucroTotalPeriodo(dataInicioStr, horaInicioStr, dataFimStr, horaFimStr);
                        textResultado.Text = $"Lucro total: €{result.ToString("F2")}";
                    }
                    else if (metodo == "GetPrejuizoPorPecaPeriodo")
                    {
                        var lista = client.GetPrejuizoPorPecaPeriodo(dataInicioStr, horaInicioStr, dataFimStr, horaFimStr);

                        if (lista == null || lista.Length == 0)
                        {
                            textResultado.Text = "Não há prejuízo nesse período de tempo.";
                        }
                        else
                        {
                            textResultado.AppendText("Prejuízo total por peça:\n");
                            foreach (var item in lista)
                            textResultado.AppendText($"{item.CodigoPeca}: €{item.PrejuizoTotal.ToString("F2")}\n");
                        }                          
                    }
                }
                else if (metodo == "GetPecaComMaiorPrejuizo")
                {
                    string result = client.GetPecaComMaiorPrejuizo();
                    textResultado.Text = string.IsNullOrWhiteSpace(result)
                                ? "Não há dados de prejuízo disponíveis."
                                : $"Peça com maior prejuízo: {result}";
                }
                else if (metodo == "GetFinanceiroPorPeca")
                {
                    string codigo = ((TextBox)pnlParametros.Controls["txtCodigoPeca"]).Text;

                    if (!Regex.IsMatch(codigo, @"^[a-b]{2}\d{6}$", RegexOptions.IgnoreCase))
                    {
                        textResultado.Text = "Erro: o código da peça deve estar no formato 'aa123456' (duas letras a/b + seis dígitos).";
                        return;
                    }

                    var dados = client.GetFinanceiroPorPeca(codigo);
                    if (dados != null)
                    {
                        textResultado.Text =
                            //$"ID Produto: {dados.ID_Produto}\n" +
                            $"Código: {dados.Codigo_Peca}\n" +              
                            $"Tempo: {dados.Tempo_Producao} s\n" +
                            $"Custo: €{dados.Custo_Producao.ToString("F2")}\n" +
                            $"Lucro: €{dados.Lucro.ToString("F2")}\n" +
                            $"Prejuízo: €{dados.Prejuizo.ToString("F2")}";
                    }
                    else
                    {
                        textResultado.Text = "Peça não encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                textResultado.Text = "Erro inesperado: " + ex.Message;
            }
        }
    }
}