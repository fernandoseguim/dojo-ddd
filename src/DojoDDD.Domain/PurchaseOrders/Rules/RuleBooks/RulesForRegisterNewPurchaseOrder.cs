using System.Collections.Generic;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.PurchaseOrders.Entities;
using DojoDDD.Domain.PurchaseOrders.Errors;

namespace DojoDDD.Domain.PurchaseOrders.Rules.RuleBooks
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