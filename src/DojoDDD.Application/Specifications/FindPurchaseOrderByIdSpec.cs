using DojoDDD.Domain.Abstractions.Entities;
using DojoDDD.Domain.Abstractions.Specifications;
using DojoDDD.Infra.DbContext.Models;

namespace DojoDDD.Application.Specifications
{
    public class FindPurchaseOrderByIdSpec : QuerySpecification<PurchaseOrderModel>
    {
        public FindPurchaseOrderByIdSpec(string id) : base(data => data.Id == IdHelper.LoadForOrders(id))
        {
        }
    }
}