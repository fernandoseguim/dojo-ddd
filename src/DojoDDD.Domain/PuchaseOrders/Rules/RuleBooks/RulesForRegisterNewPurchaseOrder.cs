using System.Collections.Generic;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Errors;

namespace DojoDDD.Domain.PuchaseOrders.Rules.RuleBooks
{
    public class RulesForRegisterNewPurchaseOrder : RuleBook<PurchaseOrder>
    {
        public RulesForRegisterNewPurchaseOrder() : base(new List<IRule<PurchaseOrder, DetailedError>>
        {
                new RequestedQuantityNotEnoughToPurchase(),
                new ProductAvailableQuantityMustBeEnough(),
                new PurchaseMinAmountNotReached(),
                new ClientAvailableBalanceNotEnough()
        }) { }
    }
}