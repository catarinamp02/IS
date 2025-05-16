namespace DataTransmitter
{
    internal class Produto
    {
        public int iD_Produto { get; set; }
        public string codigo_Peca { get; set; }
        public DateOnly data_Producao { get; set; }
        public TimeOnly hora_Producao { get; set; }
        public int tempo_Producao { get; set; }
    }
}
