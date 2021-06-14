using System.Collections.Generic;
using System.Threading.Tasks;

namespace DojoDDD.Api.DojoDDD.Domain
{
    public interface IProdutoRepositorio
    {
        Task<Produto> ConsultarPorId(int id);
        Task<IEnumerable<Produto>> Consultar();
    }
}