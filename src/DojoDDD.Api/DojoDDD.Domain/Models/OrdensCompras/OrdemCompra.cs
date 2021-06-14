using System;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public class OrdemCompra
    {
        public OrdemCompra()
        {
            Status = OrdemCompraStatus.Solicitado;
        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public DateTime DataOperacao { get; set; }
        public int ProdutoId { get; set; }
        public string ClienteId { get; set; }
        public int QuantidadeSolicitada { get; set; }
        public decimal ValorOperacao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public OrdemCompraStatus Status { get; set; }
    }
}