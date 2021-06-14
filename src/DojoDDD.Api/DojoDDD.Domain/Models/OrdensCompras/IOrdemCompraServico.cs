using System.Threading.Tasks;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public interface IOrdemCompraServico
    {
        Task<bool> AlterarStatudOrdemDeCompraParaEmAnalise(string ordemDeCompraId);
        Task<string> RegistrarOrdemCompra(string clienteId, int produtoId, int quantidadeCompra);
    }
}