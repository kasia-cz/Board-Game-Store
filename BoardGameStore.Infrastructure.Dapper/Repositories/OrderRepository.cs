using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task AddOrder(OrderModel orderModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> GetUserOrders(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
