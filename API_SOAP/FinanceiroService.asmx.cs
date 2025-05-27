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
        [WebMethod]
        public decimal GetCustoTotalPeriodo(DateTime dataInicio, TimeSpan horaInicio, DateTime dataFim, TimeSpan horaFim)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("CustoTotalPeriodo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DataInicio", dataInicio);
                cmd.Parameters.AddWithValue("@HoraInicio", horaInicio);
                cmd.Parameters.AddWithValue("@DataFim", dataFim);
                cmd.Parameters.AddWithValue("@HoraFim", horaFim);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
        }

    }
}
