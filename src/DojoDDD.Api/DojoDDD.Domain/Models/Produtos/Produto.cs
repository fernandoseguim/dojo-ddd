namespace DojoDDD.Api.DojoDDD.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Estoque { get; set; }
        public string PrecoUnitario { get; set; }
        public int ValorMinimoDeCompra { get; set; }
    }
}