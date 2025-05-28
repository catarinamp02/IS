using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            //cbMetodo.Items.Add("GetCustoTotalPeriodo");
            //cbMetodo.Items.Add("GetLucroTotalPeriodo");
            //cbMetodo.Items.Add("GetPrejuizoPorPecaPeriodo");
            //cbMetodo.Items.Add("GetPecaComMaiorPrejuizo");
            //cbMetodo.Items.Add("GetFinanceiroPorPeca");
            //cbMetodo.SelectedIndex = 0;

            foreach (var item in metodosSOAP.Keys)
                cbMetodo.Items.Add(item);

            cbMetodo.SelectedIndex = 0;
        }


        // Dicionário dos nomes dos métodos SOAP 
        private Dictionary<string, string> metodosSOAP = new Dictionary<string, string>
        {
            { "Custo Total num Período", "GetCustoTotalPeriodoTexto" },
            { "Lucro Total num Período", "GetLucroTotalPeriodoTexto" },
            { "Prejuízo por Peça num Período", "GetPrejuizoPorPecaPeriodoTexto" },
            { "Peça com Maior Prejuízo", "GetPecaComMaiorPrejuizo" },
            { "Dados Financeiros por Peça", "GetFinanceiroPorPeca" }
        };

        // Métodos auxiliares para adicionar campos de data e hora
        private void AddDateTimeField(string labelText, string controlName, int top)
        {
            pnlParametros.Controls.Add(new Label() { Text = labelText, Top = top, Left = 10 });
            pnlParametros.Controls.Add(new DateTimePicker()
            {
                Name = controlName,
                Format = DateTimePickerFormat.Short,
                Top = top + 20,
                Left = 10
            });
        }

        private void AddTimeField(string labelText, string controlName, int top)
        {
            pnlParametros.Controls.Add(new Label() { Text = labelText, Top = top, Left = 10 });
            pnlParametros.Controls.Add(new DateTimePicker()
            {
                Name = controlName,
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Top = top + 20,
                Left = 10
            });
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
            string metodo = cbMetodo.SelectedItem.ToString();

            if (metodo.Contains("Periodo"))
            {
                // 4 DateTimePickers: DataInicio, HoraInicio, DataFim, HoraFim
                AddDateTimeField("Data Início", "dtInicio", 10);
                AddTimeField("Hora Início", "horaInicio", 60);
                AddDateTimeField("Data Fim", "dtFim", 110);
                AddTimeField("Hora Fim", "horaFim", 160);
            }
            else if (metodo == "GetFinanceiroPorPeca")
            {
                pnlParametros.Controls.Add(new Label() { Text = "Código da Peça", Top = 10, Left = 10 });
                pnlParametros.Controls.Add(new TextBox() { Name = "txtCodigoPeca", Top = 30, Left = 10, Width = 150 });
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
                if (metodo == "GetCustoTotalPeriodo")
                {
                    DateTime di = GetDate("dtInicio");
                    TimeSpan hi = GetTime("horaInicio");
                    DateTime df = GetDate("dtFim");
                    TimeSpan hf = GetTime("horaFim");

                    string dataInicioStr = di.ToString("yyyy-MM-dd");
                    string horaInicioStr = hi.ToString(@"hh\:mm\:ss");
                    string dataFimStr = df.ToString("yyyy-MM-dd");
                    string horaFimStr = hf.ToString(@"hh\:mm\:ss");

                    var result = client.GetCustoTotalPeriodo(dataInicioStr, horaInicioStr, dataFimStr, horaFimStr);
                    textResultado.Text = $"Custo total: €{result}";
                }
                //else if (metodo == "getlucrototalperiodo")
                //{
                //    datetime di = getdate("dtinicio");
                //    timespan hi = gettime("horainicio");
                //    datetime df = getdate("dtfim");
                //    timespan hf = gettime("horafim");

                //    var result = client.getlucrototalperiodo(di, hi, df, hf);
                //    txtresultado.text = $"lucro total: €{result}";
                //}
                //else if (metodo == "getprejuizoporpecaperiodo")
                //{
                //    datetime di = getdate("dtinicio");
                //    timespan hi = gettime("horainicio");
                //    datetime df = getdate("dtfim");
                //    timespan hf = gettime("horafim");

                //    var lista = client.getprejuizoporpecaperiodo(di, hi, df, hf);
                //    txtresultado.appendtext("prejuízo total por peça:\n");
                //    foreach (var item in lista)
                //    {
                //        txtresultado.appendtext($"{item.codigopeca}: €{item.prejuizototal}\n");
                //    }
                //}
                //else if (metodo == "getpecacommaiorprejuizo")
                //{
                //    string result = client.getpecacommaiorprejuizo();
                //    txtresultado.text = $"peça com maior prejuízo: {result}";
                //}
                //else if (metodo == "getfinanceiroporpeca")
                //{
                //    string codigo = ((textbox)pnlparametros.controls["txtcodigopeca"]).text;
                //    var dados = client.getfinanceiroporpeca(codigo);
                //    if (dados != null)
                //    {
                //        txtresultado.text =
                //            $"código: {dados.codigo_peca}\n" +
                //            $"id produto: {dados.id_produto}\n" +
                //            $"tempo: {dados.tempo_producao} s\n" +
                //            $"custo: €{dados.custo_producao}\n" +
                //            $"lucro: €{dados.lucro}\n" +
                //            $"prejuízo: €{dados.prejuizo}";
                //    }
                //    else
                //    {
                //        txtresultado.text = "peça não encontrada.";
                //    }
                //}
            }
            catch (Exception ex)
            {
                textResultado.Text = "Erro: " + ex.Message;
            }
        }
   
    }
}
