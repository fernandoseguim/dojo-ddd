using System.Threading.Tasks;
using DojoDDD.Infra.Providers.BusinessPeriod.Models;

namespace DojoDDD.Infra.Providers.BusinessPeriod
{
    public interface IBusinessPeriodProvider
    {
        Task<BusinessPeriodModel> GetBusinessPeriodAsync();
    }
}