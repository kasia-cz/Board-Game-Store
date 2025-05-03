using BoardGameStore.Domain.Enums;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextEFCore _context;

        public OrderRepository(DbContextEFCore context)
        {
            _context = context;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            var order = MapOrderModelToEntity(orderModel);
            order.Date = DateTime.Now;
            order.Status = OrderStatus.Pending;
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            var orders = await _context.Orders.Include(o => o.Items).ToListAsync();

            return orders.Select(MapOrderEntityToModel).ToList();
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(o => o.Items).ThenInclude(i => i.BoardGame).FirstOrDefaultAsync(o => o.Id == id); // Include(o => o.orderitems) ?

            return MapOrderEntityToModel(order);
        }

        public async Task<List<OrderModel>> GetUserOrders(int userId)
        {
            var userOrders = await _context.Orders.Include(o => o.Items).Where(o => o.UserId == userId).ToListAsync(); // include items

            return userOrders.Select(MapOrderEntityToModel).ToList();
        }

        private static Order MapOrderModelToEntity(OrderModel orderModel)
        {
            var order = new Order
            {
                TotalPrice = orderModel.TotalPrice,
                UserId = orderModel.UserId,
                Items = orderModel.Items.Select(orderItem => new OrderItem
                {
                    BoardGameId = orderItem.BoardGameId,
                    Quantity = orderItem.Quantity
                }).ToList()
            };

            return order;
        }

        private static OrderModel MapOrderEntityToModel(Order order)
        {
            var orderModel = new OrderModel
            {
                Id = order.Id,
                Date = order.Date,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                UserId = order.UserId,
                Items = order.Items.Select(orderItem => new OrderItemModel
                {
                    Id = orderItem.Id,
                    Quantity = orderItem.Quantity,
                    BoardGameId = orderItem.BoardGameId,
                    BoardGame = orderItem.BoardGame == null ? null : new BoardGameModel
                    {
                        Id = orderItem.BoardGame.Id,
                        Name = orderItem.BoardGame.Name,
                        Price = orderItem.BoardGame.Price
                    }
                }).ToList()
            };

            return orderModel;
        }
    }
}
