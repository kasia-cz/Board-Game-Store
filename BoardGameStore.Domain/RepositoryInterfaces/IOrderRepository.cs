using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetAllOrders();

        Task<List<OrderModel>> GetUserOrders(int userId);

        Task<OrderModel> GetOrderById(int id);

        Task AddOrder(OrderModel orderModel);
    }
}
