using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetAllOrders();

        Task<List<OrderModel>> GetUserOrders(int userId);

        Task<OrderModel> GetOrderById(int id);

        Task AddOrder(OrderModel orderModel);
    }
}
