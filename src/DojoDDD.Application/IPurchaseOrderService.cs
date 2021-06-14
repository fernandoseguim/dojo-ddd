using System.Threading.Tasks;

namespace DojoDDD.Application
{
    public interface IPurchaseOrderService
    {
        Task<bool> ChangeStatusToAnalyzing(string orderId);
        Task<string> Register(string clientId, int productId, int orderedQuantity);
    }
}