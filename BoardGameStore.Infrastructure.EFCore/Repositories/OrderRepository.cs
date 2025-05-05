using BoardGameStore.Domain.Enums;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextEFCore _context;
        private readonly IMapper _mapper;

        public OrderRepository(DbContextEFCore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            var order = _mapper.MapOrderModelToEntity(orderModel);
            order.Date = DateTime.Now;
            order.Status = OrderStatus.Pending;
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders.Select(_mapper.MapOrderEntityToModel).ToList();
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(o => o.Items).ThenInclude(i => i.BoardGame).FirstOrDefaultAsync(o => o.Id == id);

            return _mapper.MapOrderEntityToModel(order);
        }

        public async Task<List<OrderModel>> GetUserOrders(int userId)
        {
            var userOrders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();

            return userOrders.Select(_mapper.MapOrderEntityToModel).ToList();
        }
    }
}
