using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Falhas
{
    internal class Peca
    {
        public DateOnly dataProd { get; set; }
        public TimeOnly horaProd { get; set; }
        public string codigo { get; set; }
        public int tempoProd { get; set; }
        public int resultadoTeste { get; set; }
        public string descricaoTeste { get; set; }
        public DateOnly datateste { get; set; }
    }
}
