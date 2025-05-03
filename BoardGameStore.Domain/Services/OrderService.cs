using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            await _orderRepository.AddOrder(orderModel);
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task<List<OrderModel>> GetUserOrders(int userId)
        {
            return await _orderRepository.GetUserOrders(userId);
        }
    }
}
