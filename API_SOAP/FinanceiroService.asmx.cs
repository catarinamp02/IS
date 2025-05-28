using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace API_SOAP
{
    /// <summary>
    /// Summary description for FinanceiroService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
 
    
    public class FinanceiroService : System.Web.Services.WebService
    {
        private string connStr = ConfigurationManager.ConnectionStrings["Contabilidade"].ConnectionString;

        //class aux return
        public class PecaPrejuizo
        {
            public string CodigoPeca { get; set; }
            public decimal PrejuizoTotal { get; set; }
        }

        public class PecaFinanceiroDetalhado
        {
            public int ID_Produto { get; set; }
            public string Codigo_Peca { get; set; }
            public int Tempo_Producao { get; set; }
            public decimal Custo_Producao { get; set; }
            public decimal Lucro { get; set; }
            public decimal Prejuizo { get; set; }
        }



        //Obter Peça com Maior Prejuízo:
        [WebMethod]
        public string GetPecaComMaiorPrejuizo()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("PecaComMaiorPrejuizo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader["Codigo_Peca"].ToString();
                }
            }

            return "Nenhuma peça encontrada!";
        }


        //Obter Custos Totais de Produção num período
        [WebMethod]
        public decimal GetCustoTotalPeriodo(string dataInicio, string horaInicio, string dataFim, string horaFim)
        {
            DateTime di = DateTime.Parse($"{dataInicio} {horaInicio}");
            DateTime df = DateTime.Parse($"{dataFim} {horaFim}");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("CustoTotalPeriodo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DataInicio", di.Date);
                cmd.Parameters.AddWithValue("@HoraInicio", di.TimeOfDay);
                cmd.Parameters.AddWithValue("@DataFim", df.Date);
                cmd.Parameters.AddWithValue("@HoraFim", df.TimeOfDay);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
        }


        //Obter Lucro total num Período
        [WebMethod]
        public decimal GetLucroTotalPeriodo(string dataInicio, string horaInicio, string dataFim, string horaFim)
        {
            DateTime di = DateTime.Parse($"{dataInicio} {horaInicio}");
            DateTime df = DateTime.Parse($"{dataFim} {horaFim}");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("LucroTotalPeriodo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DataInicio", di.Date);
                cmd.Parameters.AddWithValue("@HoraInicio", di.TimeOfDay);
                cmd.Parameters.AddWithValue("@DataFim", df.Date);
                cmd.Parameters.AddWithValue("@HoraFim", df.TimeOfDay);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
        }


        //Obter Prejuízo total de cada uma das peças num período
        [WebMethod]
        public List<PecaPrejuizo> GetPrejuizoPorPecaPeriodo(string dataInicio, string horaInicio, string dataFim, string horaFim)
        {
            var lista = new List<PecaPrejuizo>();
            DateTime di = DateTime.Parse($"{dataInicio} {horaInicio}");
            DateTime df = DateTime.Parse($"{dataFim} {horaFim}");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("PrejuizoPorPecaPeriodo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DataInicio", di.Date);
                cmd.Parameters.AddWithValue("@HoraInicio", di.TimeOfDay);
                cmd.Parameters.AddWithValue("@DataFim", df.Date);
                cmd.Parameters.AddWithValue("@HoraFim", df.TimeOfDay);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PecaPrejuizo
                    {
                        CodigoPeca = reader["Codigo_Peca"].ToString(),
                        PrejuizoTotal = Convert.ToDecimal(reader["Prejuizo_Total"])
                    });
                }
            }

            return lista;
        }

        //Obter Dados Financeiros Detalhados por Peça
        [WebMethod]
        public PecaFinanceiroDetalhado GetFinanceiroPorPeca(string codigoPeca)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("FinanceiroPorPeca", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodigoPeca", codigoPeca);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PecaFinanceiroDetalhado
                    {
                        Codigo_Peca = reader["Codigo_Peca"].ToString(),
                        //ID_Produto = Convert.ToInt32(reader["ID_Produto"]),
                        Tempo_Producao = Convert.ToInt32(reader["Tempo_Producao"]),
                        Custo_Producao = Convert.ToDecimal(reader["Custo_Producao"]),
                        Lucro = Convert.ToDecimal(reader["Lucro"]),
                        Prejuizo = Convert.ToDecimal(reader["Prejuizo"])
                    };
                }
            }

            return null;
        }
    }
}