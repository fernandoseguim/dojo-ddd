using DojoDDD.Domain.PurchaseOrders.Commands;

namespace DojoDDD.Api.Controllers.v1.Models
{
    public class PurchaseOrderLegacyRequest
    {
        public int ProdutoId { get; set; }
        public string ClienteId { get; set; }
        public int QuantidadeSolicitada { get; set; }

        public static implicit operator PurchaseOrderRegisterCommand(PurchaseOrderLegacyRequest request)
            => new(request.ClienteId, request.ProdutoId, request.QuantidadeSolicitada);
    }
}