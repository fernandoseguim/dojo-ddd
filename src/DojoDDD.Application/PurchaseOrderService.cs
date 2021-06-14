using System;
using System.Threading.Tasks;
using DojoDDD.Application.Specifications;
using DojoDDD.Domain.Abstractions.Repositories;
using DojoDDD.Domain.Aggregates;
using DojoDDD.Domain.Entities;

namespace DojoDDD.Application
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IQueryableRepository<Client> _clientsRepository;
        private readonly IQueryableRepository<Product> _productsRepository;
        private readonly IEntityRepository<PurchaseOrder> _orderRepository;

        public PurchaseOrderService(IQueryableRepository<Client> clientsRepository, IQueryableRepository<Product> productsRepository, IEntityRepository<PurchaseOrder> orderRepository)
        {
            _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<string> Register(string clientId, int productId, int orderedQuantity)
        {
            var client = await _clientsRepository.GetAsync(new FindClientByIdSpec(clientId)).ConfigureAwait(false);
            var product = await _productsRepository.GetAsync(new FindProductByIdSpec(productId)).ConfigureAwait(false);

            if (orderedQuantity <= 0)
                throw new InvalidOperationException("Quantidade solicitada não suficiente para compra.");

            if (product.Quantity <= 0)
                throw new InvalidOperationException("Quantidade em estoque não suficiente para compra.");

            var valorOperacao = Math.Round(product.UnitPrice * orderedQuantity, 2);
            if (valorOperacao > client.Balance)
                throw new InvalidOperationException("Cliente não possui saldo suficiente para compra.");

            if (Math.Round(orderedQuantity * product.UnitPrice, 2) < product.PurchaseMinAmount)
                throw new InvalidOperationException("Quantidade mínima não atendida para compra.");

            if (valorOperacao > product.Quantity)
                throw new InvalidOperationException("Quantidade em estoque não suficiente para compra.");

            var novaOrdemDeCompra = new PurchaseOrder
            {
                ClientId = client.Id,
                ProductId = product.Id,
                OrderDate = DateTime.Now,
                UnitPrice = product.UnitPrice,
                OperationAmount = valorOperacao,
                OrderedQuantity = orderedQuantity
            };

            await _orderRepository.SaveAsync(novaOrdemDeCompra).ConfigureAwait(false);

            return novaOrdemDeCompra.Id;
        }

        public async Task<bool> ChangeStatusToAnalyzing(string orderId)
        {
            var ordemDeCompra = await _orderRepository.GetAsync(new FindPurchaseOrderByIdSpec(orderId)).ConfigureAwait(false);

            return true;
        }
    }
}