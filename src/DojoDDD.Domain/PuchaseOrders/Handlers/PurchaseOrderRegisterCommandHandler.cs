using System;
using System.Threading.Tasks;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Clients.Entities;
using DojoDDD.Domain.Clients.Specifications;
using DojoDDD.Domain.Products.Entities;
using DojoDDD.Domain.Products.Specifications;
using DojoDDD.Domain.PuchaseOrders.Commands;
using DojoDDD.Domain.PuchaseOrders.Entities;
using DojoDDD.Domain.PuchaseOrders.Rules.RuleBooks;
using DojoDDD.Domain.PuchaseOrders.Specifications;
using FluentResults;

namespace DojoDDD.Domain.PuchaseOrders.Handlers
{
    public class PurchaseOrderRegisterCommandHandler : IPurchaseOrderRegisterCommandHandler
    {
        private readonly IQueryableRepository<Client> _clientsRepository;
        private readonly IQueryableRepository<Product> _productsRepository;
        private readonly IEntityRepository<PurchaseOrder> _orderRepository;
        private readonly RulesForRegisterNewPurchaseOrder _rules;

        public PurchaseOrderRegisterCommandHandler(IQueryableRepository<Client> clientsRepository,
                                                   IQueryableRepository<Product> productsRepository,
                                                   IEntityRepository<PurchaseOrder> orderRepository,
                                                   RulesForRegisterNewPurchaseOrder rules)
        {
            _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public async Task<Result<PurchaseOrder>> HandleAsync(PurchaseOrderRegisterCommand command)
        {
            var client = await _clientsRepository.GetAsync(new FindClientByIdSpec(command.ClientId));
            var product = await _productsRepository.GetAsync(new FindProductByIdSpec(command.ProductId));

            var order = PurchaseOrder.Create(client, product, command.RequestedQuantity);

            var result = await _rules.ApplyRules(order);

            if(result.IsFailed)
                return result;

            await _orderRepository.SaveAsync(order);

            return result;
        }

        public async Task<bool> ChangeStatusToAnalyzing(string orderId)
        {
            var ordemDeCompra = await _orderRepository.GetAsync(new FindPurchaseOrderByIdSpec(orderId));

            return true;
        }
    }
}