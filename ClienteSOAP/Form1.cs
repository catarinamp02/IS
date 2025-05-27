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
            cbMetodo.Items.Add("GetCustoTotalPeriodo");
            cbMetodo.Items.Add("GetLucroTotalPeriodo");
            cbMetodo.Items.Add("GetPrejuizoPorPecaPeriodo");
            cbMetodo.Items.Add("GetPecaComMaiorPrejuizo");
            cbMetodo.Items.Add("GetFinanceiroPorPeca");
            cbMetodo.SelectedIndex = 0;
        }

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


        private void btnExecutar_Click(object sender, EventArgs e)
        {
            var client = new FinanceiroWS.FinanceiroServiceSoapClient();
            string metodo = cbMetodo.SelectedItem.ToString();
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
                //else if (metodo == "GetLucroTotalPeriodo")
                //{
                //    DateTime di = GetDate("dtInicio");
                //    TimeSpan hi = GetTime("horaInicio");
                //    DateTime df = GetDate("dtFim");
                //    TimeSpan hf = GetTime("horaFim");

                //    var result = client.GetLucroTotalPeriodo(di, hi, df, hf);
                //    txtResultado.Text = $"Lucro total: €{result}";
                //}
                //else if (metodo == "GetPrejuizoPorPecaPeriodo")
                //{
                //    DateTime di = GetDate("dtInicio");
                //    TimeSpan hi = GetTime("horaInicio");
                //    DateTime df = GetDate("dtFim");
                //    TimeSpan hf = GetTime("horaFim");

                //    var lista = client.GetPrejuizoPorPecaPeriodo(di, hi, df, hf);
                //    txtResultado.AppendText("Prejuízo total por peça:\n");
                //    foreach (var item in lista)
                //    {
                //        txtResultado.AppendText($"{item.CodigoPeca}: €{item.PrejuizoTotal}\n");
                //    }
                //}
                //else if (metodo == "GetPecaComMaiorPrejuizo")
                //{
                //    string result = client.GetPecaComMaiorPrejuizo();
                //    txtResultado.Text = $"Peça com maior prejuízo: {result}";
                //}
                //else if (metodo == "GetFinanceiroPorPeca")
                //{
                //    string codigo = ((TextBox)pnlParametros.Controls["txtCodigoPeca"]).Text;
                //    var dados = client.GetFinanceiroPorPeca(codigo);
                //    if (dados != null)
                //    {
                //        txtResultado.Text =
                //            $"Código: {dados.Codigo_Peca}\n" +
                //            $"ID Produto: {dados.ID_Produto}\n" +
                //            $"Tempo: {dados.Tempo_Producao} s\n" +
                //            $"Custo: €{dados.Custo_Producao}\n" +
                //            $"Lucro: €{dados.Lucro}\n" +
                //            $"Prejuízo: €{dados.Prejuizo}";
                //    }
                //    else
                //    {
                //        txtResultado.Text = "Peça não encontrada.";
                //    }
                //}
            }
            catch (Exception ex)
            {
                textResultado.Text = "Erro: " + ex.Message;
            }
        }

        private DateTime GetDate(string controlName) =>
              ((DateTimePicker)pnlParametros.Controls[controlName]).Value.Date;

        private TimeSpan GetTime(string controlName) =>
            ((DateTimePicker)pnlParametros.Controls[controlName]).Value.TimeOfDay;

    }
}
