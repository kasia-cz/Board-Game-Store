using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping
{
    public class ManualMapper : IMapper
    {
        // map board game
        public BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame)
        {
            return new BoardGameModel
            {
                Id = boardGame.Id,
                Name = boardGame.Name,
                Year = boardGame.Year,
                MinPlayers = boardGame.MinPlayers,
                MaxPlayers = boardGame.MaxPlayers,
                Difficulty = boardGame.Difficulty,
                AvailableQuantity = boardGame.AvailableQuantity,
                Price = boardGame.Price
            };
        }

        public BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel)
        {
            return new BoardGame
            {
                Name = boardGameModel.Name,
                Year = boardGameModel.Year,
                MinPlayers = boardGameModel.MinPlayers,
                MaxPlayers = boardGameModel.MaxPlayers,
                Difficulty = boardGameModel.Difficulty,
                AvailableQuantity = boardGameModel.AvailableQuantity,
                Price = boardGameModel.Price
            };
        }

        // map order
        public OrderModel MapOrderEntityToModel(Order order)
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

        public Order MapOrderModelToEntity(OrderModel orderModel)
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

        // map user
        public UserModel MapUserEntityToModel(User user)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
            };

            if (user.Address != null)
            {
                userModel.Address = new AddressModel
                {
                    Id = user.Address.Id,
                    City = user.Address.City,
                    AddressLine = user.Address.AddressLine,
                    PostalCode = user.Address.PostalCode,
                };
            }

            return userModel;
        }

        public User MapUserModelToEntity(UserModel userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                DateOfBirth = userModel.DateOfBirth,
            };

            if (userModel.Address != null)
            {
                user.Address = new Address
                {
                    City = userModel.Address.City,
                    AddressLine = userModel.Address.AddressLine,
                    PostalCode = userModel.Address.PostalCode,
                };
            }

            return user;
        }
    }
}
