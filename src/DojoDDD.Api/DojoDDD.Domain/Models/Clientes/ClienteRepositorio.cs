using DojoDDD.Api.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly DataStore _dataStore;

        public ClienteRepositorio(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<Cliente> ConsultarPorId(string id)
        {
            var cliente = _dataStore.Clientes.Find(x => x.Id.Equals(id));
            return await Task.FromResult(cliente).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Cliente>> ConsultarTodosCliente() 
            => await Task.FromResult(_dataStore.Clientes).ConfigureAwait(false);
    }
}