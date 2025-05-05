using BoardGameStore.Domain.Enums;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Entities;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Dapper;
using System.Data;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public OrderRepository(IDbConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            var order = _mapper.MapOrderModelToEntity(orderModel);
            order.Date = DateTime.Now;
            order.Status = OrderStatus.Pending;

            const string orderSql = @"
            INSERT INTO Orders (Date, TotalPrice, Status, UserId)
            VALUES (@Date, @TotalPrice, @Status, @UserId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            var orderId = await _connection.QuerySingleAsync<int>(orderSql, order);

            foreach (var item in order.Items)
            {
                item.OrderId = orderId;

                const string itemSql = @"
                INSERT INTO OrderItems (Quantity, BoardGameId, OrderId)
                VALUES (@Quantity, @BoardGameId, @OrderId);";

                await _connection.ExecuteAsync(itemSql, item);
            }
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            const string sql = "SELECT * FROM Orders;";
            var orders = await _connection.QueryAsync<Order>(sql);

            return orders.Select(_mapper.MapOrderEntityToModel).ToList();
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            Order order = null;

            const string sql = @"
            SELECT o.*, i.*, b.*
            FROM Orders o
            LEFT JOIN OrderItems i ON o.Id = i.OrderId
            LEFT JOIN BoardGames b ON i.BoardGameId = b.Id
            WHERE o.Id = @Id;";

            await _connection.QueryAsync<Order, OrderItem, BoardGame, Order>(sql, (o, oi, bg) =>
                {
                    if (order == null)
                    {
                        order = o;
                        order.Items = new List<OrderItem>();
                    }

                    oi.BoardGame = bg;
                    order.Items.Add(oi);

                    return order;
                },
                param: new { Id = id },
                splitOn: "Id,Id"
            );

            return _mapper.MapOrderEntityToModel(order);
        }

        public async Task<List<OrderModel>> GetUserOrders(int userId)
        {
            const string sql = "SELECT * FROM Orders WHERE UserId = @UserId;";
            var orders = await _connection.QueryAsync<Order>(sql, new { UserId = userId });

            return orders.Select(_mapper.MapOrderEntityToModel).ToList();
        }
    }
}
