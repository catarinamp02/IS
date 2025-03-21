using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Testes
    {
        public int ID_Teste { get; set; }

        public int ID_Produto { get; set; }

        public string Codigo_Resultado { get; set; }

        public DateOnly Data_Teste { get; set; }
    }
}
