using DojoDDD.Api.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly DataStore _dataStore;

        public ProdutoRepositorio(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<Produto>> Consultar()
        {
            return await Task.FromResult(_dataStore.Produtos).ConfigureAwait(false);
        }

        public async Task<Produto> ConsultarPorId(int id)
        {
            return await Task.FromResult(_dataStore.Produtos.Find(x => x.Id.Equals(id))).ConfigureAwait(false);
        }
    }
}