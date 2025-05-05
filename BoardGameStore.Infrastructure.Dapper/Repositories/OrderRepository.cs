using BoardGameStore.Domain.Enums;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Entities;
using Dapper;
using System.Data;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _connection;

        public OrderRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            var order = MapOrderModelToEntity(orderModel);
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

            return orders.Select(MapOrderEntityToModel).ToList();
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

            return MapOrderEntityToModel(order);
        }

        public async Task<List<OrderModel>> GetUserOrders(int userId)
        {
            const string sql = "SELECT * FROM Orders WHERE UserId = @UserId;";
            var orders = await _connection.QueryAsync<Order>(sql, new { UserId = userId });

            return orders.Select(MapOrderEntityToModel).ToList();
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
                Items = order.Items?.Select(orderItem => new OrderItemModel
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
