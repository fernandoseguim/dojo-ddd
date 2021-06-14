using System;
using System.Threading.Tasks;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public class OrdemCompraServico : IOrdemCompraServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IOrdemCompraRepositorio _ordemCompraRepositorio;

        public OrdemCompraServico(IClienteRepositorio clienteRepositorio,
                                  IProdutoRepositorio produtoRepositorio,
                                  IOrdemCompraRepositorio ordemCompraRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _ordemCompraRepositorio = ordemCompraRepositorio;
        }

        public async Task<string> RegistrarOrdemCompra(string clienteId, int produtoId, int quantidadeCompra)
        {
            var cliente = await _clienteRepositorio.ConsultarPorId(clienteId).ConfigureAwait(false);
            var produto = await _produtoRepositorio.ConsultarPorId(produtoId).ConfigureAwait(false);

            if (quantidadeCompra <= 0)
                throw new InvalidOperationException("Quantidade solicitada não suficiente para compra.");

            if (produto.Estoque <= 0)
                throw new InvalidOperationException("Quantidade em estoque não suficiente para compra.");

            var valorOperacao = Math.Round(decimal.Parse(produto.PrecoUnitario) * quantidadeCompra, 2);
            if (valorOperacao > cliente.Saldo)
                throw new InvalidOperationException("Cliente não possui saldo suficiente para compra.");

            if (Math.Round(quantidadeCompra * decimal.Parse(produto.PrecoUnitario), 2) < produto.ValorMinimoDeCompra)
                throw new InvalidOperationException("Quantidade mínima não atendida para compra.");

            if (valorOperacao > produto.Estoque)
                throw new InvalidOperationException("Quantidade em estoque não suficiente para compra.");

            var novaOrdemDeCompra = new OrdemCompra
            {
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                DataOperacao = DateTime.Now,
                PrecoUnitario = decimal.Parse(produto.PrecoUnitario),
                ValorOperacao = valorOperacao,
                QuantidadeSolicitada = quantidadeCompra
            };

            return await _ordemCompraRepositorio.RegistrarOrdemCompra(novaOrdemDeCompra).ConfigureAwait(false);
        }

        public async Task<bool> AlterarStatudOrdemDeCompraParaEmAnalise(string ordemDeCompraId)
        {
            var ordemDeCompra = await _ordemCompraRepositorio.ConsultarPorId(ordemDeCompraId).ConfigureAwait(false);
            if (string.IsNullOrEmpty(ordemDeCompra))
                throw new InvalidOperationException("");

            try
            {
                await _ordemCompraRepositorio.AlterarOrdemCompra(ordemDeCompra, OrdemCompraStatus.EmAnalise).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}