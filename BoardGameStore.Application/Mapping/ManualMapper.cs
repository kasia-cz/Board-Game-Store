using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Mapping
{
    public class ManualMapper : IMapper
    {
        // map board game
        public BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO)
        {
            return new BoardGameModel
            {
                Name = addBoardGameDTO.Name,
                Year = addBoardGameDTO.Year,
                MinPlayers = addBoardGameDTO.MinPlayers,
                MaxPlayers = addBoardGameDTO.MaxPlayers,
                Difficulty = addBoardGameDTO.Difficulty,
                AvailableQuantity = addBoardGameDTO.AvailableQuantity,
                Price = addBoardGameDTO.Price
            };
        }

        public ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel)
        {
            return new ReturnBoardGameDTO
            {
                Id = boardGameModel.Id,
                Name = boardGameModel.Name,
                Year = boardGameModel.Year,
                PlayersNumber = $"{boardGameModel.MinPlayers}-{boardGameModel.MaxPlayers}",
                Difficulty = boardGameModel.Difficulty.ToString(),
                IsAvailable = boardGameModel.AvailableQuantity > 0,
                Price = boardGameModel.Price
            };
        }

        public ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel)
        {
            return new ReturnBoardGameShortDTO
            {
                Id = boardGameModel.Id,
                Name = boardGameModel.Name,
                Price = boardGameModel.Price
            };
        }

        // map order
        public OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO)
        {
            var OrderModel = new OrderModel
            {
                TotalPrice = addOrderDTO.TotalPrice,
                UserId = addOrderDTO.UserId,
                Items = addOrderDTO.Items.Select(orderItem => new OrderItemModel
                {
                    BoardGameId = orderItem.BoardGameId,
                    Quantity = orderItem.Quantity
                }).ToList()
            };

            return OrderModel;
        }

        public ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel)
        {
            var OrderDTO = new ReturnOrderDTO
            {
                Id = orderModel.Id,
                Date = orderModel.Date,
                TotalPrice = orderModel.TotalPrice,
                Status = orderModel.Status.ToString(),
                UserId = orderModel.UserId,
                Items = orderModel.Items.Select(orderItem => new ReturnOrderItemDTO
                {
                    Id = orderItem.Id,
                    Quantity = orderItem.Quantity,
                    BoardGame = new ReturnBoardGameShortDTO
                    {
                        Id = orderItem.BoardGame.Id,
                        Name = orderItem.BoardGame.Name,
                        Price = orderItem.BoardGame.Price
                    }
                }).ToList()
            };

            return OrderDTO;
        }

        public ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel)
        {
            return new ReturnOrderShortDTO
            {
                Id = orderModel.Id,
                Date = orderModel.Date,
                TotalPrice = orderModel.TotalPrice,
                Status = orderModel.Status.ToString(),
                UserId = orderModel.UserId
            };
        }

        // map user
        public UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO)
        {
            var userModel = new UserModel
            {
                FirstName = addUserDTO.FirstName,
                LastName = addUserDTO.LastName,
                Email = addUserDTO.Email,
                PhoneNumber = addUserDTO.PhoneNumber,
                DateOfBirth = addUserDTO.DateOfBirth,
                Address = new AddressModel
                {
                    City = addUserDTO.Address.City,
                    AddressLine = addUserDTO.Address.AddressLine,
                    PostalCode = addUserDTO.Address.PostalCode,
                }
            };

            return userModel;
        }

        public ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel)
        {
            var userDTO = new ReturnUserDTO
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                DateOfBirth = userModel.DateOfBirth,
                Address = new ReturnAddressDTO
                {
                    Id = userModel.Address.Id,
                    City = userModel.Address.City,
                    AddressLine = userModel.Address.AddressLine,
                    PostalCode = userModel.Address.PostalCode,
                }
            };

            return userDTO;
        }

        public ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel)
        {
            return new ReturnUserShortDTO
            {
                Id = userModel.Id,
                Name = $"{userModel.FirstName} {userModel.LastName}",
                Email = userModel.Email
            };
        }
    }
}
