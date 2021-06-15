using System.Collections.Generic;
using DojoDDD.Domain.Abstractions.Rules;
using DojoDDD.Domain.Entities;
using DojoDDD.Domain.Errors;

namespace DojoDDD.Domain.Rules.RuleBooks
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